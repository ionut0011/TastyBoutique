import { Component, OnInit, OnDestroy } from '@angular/core';
import {Router} from '@angular/router';
import { AuthentificationService } from '../services/authentification.service';
import { LoginModel } from '../models/login.model';
import { UserService } from 'src/app/shared/services';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [AuthentificationService],
})
export class LoginComponent  {


  public form: FormGroup;

  constructor(
    private readonly authentificationService:AuthentificationService,
    private router: Router,
    private readonly userService:UserService,
    private readonly formBuilder: FormBuilder
    ) {

      this.form = new FormGroup({
        email: new FormControl('', [Validators.required]),
        password: new FormControl(null),
      });
      this.userService.username.next('');
     }


  public clickedLogin():void{

    const data: LoginModel = this.form.getRawValue();

    this.authentificationService.login(data).subscribe((logData:any) => {
      localStorage.setItem('userToken', JSON.stringify(logData.token));
      localStorage.setItem('email', JSON.stringify(logData.email));
      this.userService.username.next(data.email.split('@')[0]);
      this.router.navigate(['dashboard']);
    });
  }

  public goToPage(page: string): void {
    this.router.navigate([page]);
  }


}
