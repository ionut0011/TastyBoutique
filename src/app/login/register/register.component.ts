import { Component, OnInit,OnDestroy } from '@angular/core';
import { LoginModel } from '../models/login.model';
import { RegisterModel } from '../models/register.model';
import { AuthentificationService } from '../services/authentification.service';
import {Router} from '@angular/router'
import { Subscription } from 'rxjs';
import { UserService } from '../../shared/services';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import {Validators} from '@angular/forms'
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent  {

  public isSetRegistered: boolean = false;
  public isAdmin:boolean=true;
  public formGroup: FormGroup;
  public email: string = null;
  public password: string;
  public fullName: string;
  public age : number;
  public userName: number;
  public form: FormGroup;

  constructor(
    private readonly authentificationService:AuthentificationService,
    private router:Router,
    private readonly userService:UserService,
    private readonly formBuilder: FormBuilder
    )

    {

      this.form = new FormGroup({
        email : new FormControl('',[]),
        password :  new FormControl('',[Validators.required]),
        fullName :  new FormControl('',[]),
        age : new FormControl('',[]),

      })

    this.userService.username.next('');
  }

  public setRegister(): void {
    this.isSetRegistered = !this.isSetRegistered;
  }

  setAdmin(): void {
    this.isAdmin = !this.isAdmin;
  }

  authenticate() :void
  {

    // const data: RegisterModel = {
    //   name: this.fullNameControl.value,
    //   age: this.ageControl.value,
    //   username:this.userNameControl.value,
    //   email: this.emailControl.value,
    //   password: this.passwordControl.value,
    // }
    // this.authentificationService.register(data).subscribe(() => {
    //   this.userService.username.next(data.email);
    //   this.router.navigate(['dashboard']);
    // });
    console.log(this.form.value);
    console.log(this.form.valid);
  }


  public goToPage(page: string): void {
    this.router.navigate([page]);
  }

}
