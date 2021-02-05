import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HOMEComponent } from 'src/pages/home/home.component';
import { LOGINComponent } from '../pages/login/login.component';
import { REGISTERComponent } from '../pages/register/register.component';
import { CartoesComponent } from '../pages/cartoes/cartoes.component';
import { MeusCursosComponent } from '../pages/meusCursos/meusCursos.component';


const routes: Routes = [
  { path: '', component: LOGINComponent },
  { path: 'home', component: HOMEComponent },
  { path: 'register', component: REGISTERComponent },
  { path: 'cartoes', component: CartoesComponent },
  { path: 'meuscursos', component: MeusCursosComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
