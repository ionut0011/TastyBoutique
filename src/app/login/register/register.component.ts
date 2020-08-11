import { Component} from '@angular/core';
import { RegisterModel } from '../models/register.model';
import { AuthentificationService } from '../services/authentification.service';
import {Router} from '@angular/router'
import { UserService } from '../../shared/services';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import {Validators} from '@angular/forms'
import {ToastrService} from 'ngx-toastr'

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent  {

  public isSetRegistered: boolean = false;
  public formGroup: FormGroup;
  public email: string = null;
  public password: string;
  public fullName: string;
  public age : number;
  public userName: string;
  public form: FormGroup;

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

  constructor(
    private toastr: ToastrService,
    private readonly authentificationService:AuthentificationService,
    private router:Router,
    private readonly userService:UserService,
    private readonly formBuilder: FormBuilder
    ){
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

  ClickRegister() :void
  {
    const data: RegisterModel = {
      name: this.fullNameControl.value,
      age: this.ageControl.value,
      username:this.userNameControl.value,
      email: this.emailControl.value,
      password: this.passwordControl.value,
    }
    this.authentificationService.register(data).subscribe((logData:any) => {
      this.toastr.success("Successfully registered");
      this.router.navigate(['login']);
    },
    (error)=>{
      this.toastr.error("Something went wrong");

    });
    console.log(this.form.value);
    console.log(this.form.valid);
  }

  public goToPage(page: string): void {
    this.router.navigate([page]);
  }



}
