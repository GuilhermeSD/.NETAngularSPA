import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventoService } from '../_services/evento.service';
import { Evento } from '../_models/Evento';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { defineLocale, BsLocaleService, ptBrLocale} from 'ngx-bootstrap';
import { observable, Observable } from 'rxjs';
defineLocale('pt-br', ptBrLocale);

import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  _filtroLista: string;
  fileNameToUpload: string;
  get filtroLista(): string {
    return this._filtroLista;
  }

  set filtroLista(value: string) {
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista? this.filtrarEventos(this.filtroLista):this.eventos;
  }

  eventosFiltrados: Evento[];
  eventos: Evento[];
  evento: Evento;
  imagemlargura = 50;
  imagemmargem = 2;
  mostrarImagem = false;
  bodyDeletarEvento = '';
  titulo = 'Eventos';
  modalRef: BsModalRef;
  registerForm: FormGroup;
  file: File;

  constructor(
    private eventoServices: EventoService
    , private fb: FormBuilder
    , private localeService: BsLocaleService
    , private toastr: ToastrService
   ) { 
     this.localeService.use('pt-br');
    }

  openModal(template: any) {
    this.registerForm.reset();    
    template.show();
  }

  openModalEdit(template: any, model: Evento) {
    this.registerForm.reset();    
    template.show();
    this.evento = Object.assign({}, model);
    this.fileNameToUpload = model.imagemURL.toString();
    this.evento.imagemURL = ''; //Gambi pra corrigir o bind das imagens
    this.registerForm.patchValue(model);
  }

  ngOnInit() {
    this.validation();
    this.getEventos();
  }

  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }

  validation() {
    this.registerForm = this.fb.group({
      id: [''],
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
      imagemURL: ['', Validators.required],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
    });
  }

  onFileChange(event) {
    this.file = event.target.files;
  }

  upload() {
    const nomeArquivo = this.evento.imagemURL.split('\\', 3);
    this.evento.imagemURL = nomeArquivo[2];
    this.eventoServices.postUpload(this.file, this.evento.imagemURL).subscribe();
  }

  salvarAlteracao(template: any) {
    if(this.registerForm.valid) {
      this.evento = Object.assign({}, this.registerForm.value);
      
      this.upload();
      
      let obs = new Observable<Object>();
      obs = this.eventoServices.postEvento(this.evento);
      if(this.evento.id) {
        obs = this.eventoServices.putEvento(this.evento);
      }
      obs.subscribe(
        (novoEvento: Evento) => {
          console.log(novoEvento);
          template.hide();
          this.getEventos();
        }, error => {
          console.log(error);
        }
      );
    }
  }

  filtrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  getEventos() {
    this.eventoServices.getAllEvento().subscribe((_eventos: Evento[]) => {
      this.eventos = _eventos;
    }, error => {
      console.log(error);
    });
  }

  excluirEvento(evento: Evento, template: any) {
    this.openModal(template);
    this.evento = evento;
    this.bodyDeletarEvento = `Tem certeza que deseja excluir o Evento: ${evento.tema}, Código: ${evento.tema}`;
  }
  
  confirmeDelete(template: any) {
    this.eventoServices.deleteEvento(this.evento.id).subscribe(
      () => {
          template.hide();
          this.getEventos();
          this.toastr.success('Removido com sucesso', 'Remover Evento');
        }, error => {
          this.toastr.error('Ocorreu um erro na sua solicitação', 'Remover Evento');
          console.log(error);
        }
    );
  }

}
