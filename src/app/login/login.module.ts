import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoginRoutingModule } from './login-routing.module';
import { LoginComponent } from './login/login.component';
import {FormsModule} from '@angular/forms'
import {SharedModule} from '../shared/shared.module';
import { RegisterComponent } from './register/register.component'

@NgModule({
  declarations: [LoginComponent, RegisterComponent],
  imports: [
    CommonModule,
    LoginRoutingModule,
    FormsModule,
    SharedModule
  ],
  exports:[
    LoginComponent,
    RegisterComponent

  ]
})
export class LoginModule { }
