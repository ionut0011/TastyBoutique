import { Component, OnInit,OnDestroy } from '@angular/core';
import { LoginModel } from '../models/login.model';
import { RegisterModel } from '../models/register.model';
import { AuthentificationService } from '../services/authentification.service';
import {Router} from '@angular/router'
import { Subscription } from 'rxjs';
import { UserService } from '../../shared/user.service';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent implements OnDestroy {
  private subscription:Subscription;
  public username:string=null;
  public email:string=null;
  public password:string=null;
  public name:string=null;
  public age:number=null;
  public isAdmin:boolean=true;




  constructor(
    private readonly authentificationService:AuthentificationService,
    private router:Router,
    private readonly userService:UserService
    )
    { this.subscription=new Subscription();}



  setAdmin(): void {
    this.isAdmin = !this.isAdmin;
  }

  authenticate() :void
  {
    const registerModel:RegisterModel={

    name:this.name,
    age:this.age,
    username:this.username,
    email:this.email,
    password:this.password,
    };

    this.subscription.add(
      this.authentificationService.register(registerModel).subscribe( (data)=>{
        this.router.navigate(['dashboard']);
        this.userService.email.next(registerModel.email);
    })
    );
  }

  ngOnDestroy(): void{
      this.subscription.unsubscribe();
  }



  public goToPage(page: string): void {
    this.router.navigate([page]);
  }

}

