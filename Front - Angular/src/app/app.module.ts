import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HttpClient } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HOMEComponent } from '../pages/home/home.component';
import { LOGINComponent } from '../pages/login/login.component';
import { REGISTERComponent } from '../pages/register/register.component';
import { CartoesComponent } from '../pages/cartoes/cartoes.component';
import { MeusCursosComponent } from '../pages/meusCursos/meusCursos.component';


import { ApiProvider } from '../providers/api/api';
import { AutenticacaoProvider } from '../providers/autenticacao/autenticacao';
import { UserProvider } from '../providers/user/user';
import { CursoProvider } from '../providers/cursos/cursos';
import { CartoesProvider } from '../providers/cartoes/cartoes'


import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {MatDialogModule} from '@angular/material/dialog';
import {MatCardModule} from '@angular/material/card';
import { AlertDialogComponent } from '../components/alert-dialog/alert-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    HOMEComponent,
    LOGINComponent,
    REGISTERComponent,
    AlertDialogComponent,
    MeusCursosComponent,
    CartoesComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatProgressSpinnerModule,
    MatDialogModule,
    MatCardModule
  ],
  providers: [
    ApiProvider,
    AutenticacaoProvider,
    UserProvider,
    CursoProvider,
    CartoesProvider
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
