import { Component, OnInit, OnDestroy } from '@angular/core';
import {Router} from '@angular/router';
import { AuthentificationService } from '../services/authentification.service';
import { LoginModel } from '../models/login.model';
import { UserService } from 'src/app/shared/services';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import * as JWT from 'jwt-decode';
import {ToastrService} from 'ngx-toastr'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [AuthentificationService],
})
export class LoginComponent  {


  public form: FormGroup;

  loggedIn: boolean = true;
  constructor(
    private toastr: ToastrService,

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

    this.authentificationService.login(data).subscribe(
      (logData:any) => {
        localStorage.setItem('userToken', JSON.stringify(logData.token));
        localStorage.setItem('email', JSON.stringify(logData.email));
        this.userService.username.next(logData.username);
        let decoded = JWT(logData.token);
        localStorage.setItem('idUser', decoded['idUse']);
        this.router.navigate(['dashboard']);
        this.loggedIn = true;
        if(this.loggedIn)
        {
          this.toastr.success("Successfully logged in");
        }
      },
      (error) => {
        this.loggedIn = false;
        this.toastr.error("Wrong email or password");
      }
    );

    console.log(this.loggedIn);
  }

  errorMessage() :void
  {
    if(this.loggedIn == false)
    {
      this.toastr.error("Wrong email or password");
    }
  }

  public goToPage(page: string): void {
    this.router.navigate([page]);
  }


}
