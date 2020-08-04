import { Component, OnInit,OnDestroy } from '@angular/core';
import { LoginModel } from '../models/login.model';
import { RegisterModel } from '../models/register.model';
import { AuthentificationService } from '../services/authentification.service';
import {Router} from '@angular/router'
import { Subscription } from 'rxjs';
import { UserService } from '../../shared/services';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent  {


  public isAdmin:boolean=true;
  public formGroup: FormGroup;



  constructor(
    private readonly authentificationService:AuthentificationService,
    private router:Router,
    private readonly userService:UserService,
    private readonly formBuilder: FormBuilder
    )
    { this.formGroup = this.formBuilder.group({
      username: new FormControl(null),
      email: new FormControl(null),
      password: new FormControl(null),
      name: new FormControl(null),
      age: new FormControl(null),
    });
    this.userService.username.next('');}



  setAdmin(): void {
    this.isAdmin = !this.isAdmin;
  }

  authenticate() :void
  {

    const data: RegisterModel = this.formGroup.getRawValue();

    this.authentificationService.register(data).subscribe((logData:any) => {
      this.userService.username.next(data.email);
      localStorage.setItem('userToken', JSON.stringify(logData.token));
      localStorage.setItem('email', JSON.stringify(logData.email));
      this.router.navigate(['dashboard']);
    });


  }



  public goToPage(page: string): void {
    this.router.navigate([page]);
  }

}
