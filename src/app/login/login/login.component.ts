import { Component, OnInit, OnDestroy } from '@angular/core';
import {Router} from '@angular/router';
import { AuthentificationService } from '../services/authentification.service';
import { Subscription } from 'rxjs';
import { LoginModel } from '../models/login.model';
import { RecoverModel } from '../models/recover.model';
import { UserService } from '../../shared/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit,OnDestroy {
  private subscription:Subscription;
  public email:string=null;
  public password:string=null;
  public newPassword:string=null;
  public showMyMessage = false;
  public click : boolean = false;
  isForgotten=false;
  isLogin=true;

  constructor(
    private readonly authentificationService:AuthentificationService,
    private router: Router,
    private readonly userService:UserService
    ) {
      this.subscription=new Subscription();
     }



  forgotPassword(): void{
    this.isForgotten=!this.isForgotten;
    this.isLogin=!this.isLogin;


  }

  ngOnInit(): void {
  }

  ngOnDestroy():void
  {
    console.log("clicked login");
  }

  clickedLogin():void{

    if(this.isLogin)
    {
    const loginModel:LoginModel={

    email:this.email,
    password:this.password,
    };

    this.subscription.add(
      this.authentificationService.login(loginModel).subscribe( (data)=>{
        this.router.navigate(['dashboard']);
        this.userService.email.next(loginModel.email);
    })
    );}
    else
    {

      const recoverModel:RecoverModel={

        email:this.email,
        newPassword:this.newPassword,
        };

        this.subscription.add(
          this.authentificationService.recover(recoverModel).subscribe( (data)=>{

            this.userService.email.next(recoverModel.email);
        })
        );

        this.click = !this.click;



    }
  }

  public goToPage(page: string): void {
    this.router.navigate([page]);
  }


}
