import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LoginRoutingModule } from './login-routing.module';
import { LoginComponent } from './login/login.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms'
import {SharedModule} from '../shared/shared.module';
import { RegisterComponent } from './register/register.component'
import { HttpClientModule } from '@angular/common/http';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { RecoverComponent } from './recover/recover.component';
@NgModule({
  declarations: [LoginComponent, RegisterComponent, RecoverComponent],
  imports: [
    CommonModule,
    LoginRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    SharedModule
  ],
  exports:[
    LoginComponent,
    RegisterComponent

  ]
})
export class LoginModule { }
