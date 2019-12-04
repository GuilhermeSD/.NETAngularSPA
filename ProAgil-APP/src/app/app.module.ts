import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EventosComponent } from './eventos/eventos.component';
import { HttpClientModule } from '@angular/common/http';
import { NavComponent } from './nav/nav.component';
import { FormsModule } from '@angular/forms';
import { DateTimeFormatPipePipe } from './_helps/DateTimeFormatPipe.pipe';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ModalModule } from 'ngx-bootstrap/modal';

@NgModule({
   declarations: [
      AppComponent,
      EventosComponent,
      NavComponent,
      DateTimeFormatPipePipe
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      FormsModule,
      BsDropdownModule.forRoot(),
      TooltipModule.forRoot(),
      ModalModule.forRoot()
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
