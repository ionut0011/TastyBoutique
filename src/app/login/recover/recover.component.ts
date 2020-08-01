import { Component, OnInit, OnDestroy } from '@angular/core';
import {Router} from '@angular/router';
import { AuthentificationService } from '../services/authentification.service';
import { Subscription } from 'rxjs';
import { LoginModel } from '../models/login.model';
import { RecoverModel } from '../models/recover.model';
import { UserService } from '../../shared/user.service';

@Component({
  selector: 'app-recover',
  templateUrl: './recover.component.html',
  styleUrls: ['./recover.component.css']
})

export class RecoverComponent implements OnInit,OnDestroy {
  private subscription:Subscription;
  public email:string=null;
  public newPassword:string=null;


  constructor(
    private readonly authentificationService:AuthentificationService,
    private router: Router,
    private readonly userService:UserService
    ) {
      this.subscription=new Subscription();
     }


  ngOnInit(): void {
  }

  ngOnDestroy():void
  {
    console.log("clicked login");
  }

  clickedLogin():void{


    console.log('s-a apasat change password');
      const recoverModel:RecoverModel={

        email:this.email,
        newPassword:this.newPassword,
        };

        this.subscription.add(
          this.authentificationService.recover(recoverModel).subscribe( (data)=>{
            this.router.navigate(['login']);

            //this.userService.email.next(recoverModel.email);
        })
        );

  }

  public goToPage(page: string): void {
    this.router.navigate([page]);
  }


}

