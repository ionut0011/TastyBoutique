import { Component, OnInit, OnDestroy } from '@angular/core';
import {Router} from '@angular/router';
import { AuthentificationService } from '../services/authentification.service';
import { LoginModel } from '../models/login.model';
import { UserService } from 'src/app/shared/services';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [AuthentificationService],
})
export class LoginComponent  {


  public formGroup: FormGroup;

  constructor(
    private readonly authentificationService:AuthentificationService,
    private router: Router,
    private readonly userService:UserService,
    private readonly formBuilder: FormBuilder
    ) {
      this.formGroup = this.formBuilder.group({
        email: new FormControl(null),
        password: new FormControl(null),
      });
      this.userService.username.next('');
     }




  public clickedLogin():void{


    const data: LoginModel = this.formGroup.getRawValue();

    this.authentificationService.login(data).subscribe(() => {
      this.userService.username.next(data.email);
      this.router.navigate(['dashboard']);
    });

  }

  public goToPage(page: string): void {
    this.router.navigate([page]);
  }


}
