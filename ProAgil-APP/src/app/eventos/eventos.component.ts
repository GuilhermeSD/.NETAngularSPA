import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventoService } from '../_services/evento.service';
import { Evento } from '../_models/Evento';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  _filtroLista: string;
  get filtroLista(): string {
    return this._filtroLista;
  }

  set filtroLista(value: string) {
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista? this.filtrarEventos(this.filtroLista):this.eventos;
  }

  eventosFiltrados: Evento[];
  eventos: Evento[];
  imagemlargura = 50;
  imagemmargem = 2;
  mostrarImagem = false;

  modalRef: BsModalRef;

  constructor(
    private eventoServices: EventoService,
    private modalService: BsModalService) { 
      
    }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template); 
  }

  ngOnInit() {
    this.getEventos();
  }

  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
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

}
