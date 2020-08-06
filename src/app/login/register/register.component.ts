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
  public userName: string;

  public form: FormGroup;

  constructor(
    private readonly authentificationService:AuthentificationService,
    private router:Router,
    private readonly userService:UserService,
    private readonly formBuilder: FormBuilder
    )

    {

      this.form = new FormGroup({
        email : new FormControl('',[Validators.required, Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$")]),
        password :  new FormControl('',[Validators.required, Validators.minLength(8), Validators.pattern("(.*[A-Z].*[0-9].*)|(.*[0-9].*[A-Z].*)")]),
        fullName :  new FormControl('',[]),
        userName: new FormControl('', [Validators.required]),
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

    console.log("ai dat click pe submit")

    const data: RegisterModel = {
      name: this.fullNameControl.value,
      age: this.ageControl.value,
      username:this.userNameControl.value,
      email: this.emailControl.value,
      password: this.passwordControl.value,
    }
    this.authentificationService.register(data).subscribe((logData:any) => {

      this.router.navigate(['login']);
    });
    console.log(this.form.value);
    console.log(this.form.valid);
  }


  public goToPage(page: string): void {
    this.router.navigate([page]);
  }

  public get ageControl() :FormControl{
    return this.form.controls.age as FormControl;
  }

  public get fullNameControl() :FormControl{
    return this.form.controls.fullName as FormControl;
  }

  public get userNameControl() :FormControl{
    return this.form.controls.userName as FormControl;
  }
  public get emailControl() :FormControl{
    return this.form.controls.email as FormControl;
  }

  public get passwordControl() :FormControl{
    return this.form.controls.password as FormControl;
  }

  public get isFormValid(): boolean{
    return this.form.valid;
  }

}
