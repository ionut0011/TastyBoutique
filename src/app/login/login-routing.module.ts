import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {LoginComponent} from './login/login.component'
import {RegisterComponent} from './register/register.component'
import {RecoverComponent} from './recover/recover.component'

const routes: Routes = [
  {
    path:'login',
    pathMatch:'full',
    component:LoginComponent,
  },
  {
    path:'register',
    pathMatch:'full',
    component:RegisterComponent,
  },
  {
    path:'recover',
    pathMatch:'full',
    component:RecoverComponent,
  },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LoginRoutingModule { }
