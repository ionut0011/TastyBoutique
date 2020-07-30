import { Component, OnInit, OnDestroy } from '@angular/core';
import {Router} from '@angular/router'


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit,OnDestroy {

  public email:string=null;
  public password:string=null;

  isForgotten=false;
  isLogin=true;

  constructor(private router: Router) { }



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
    this.router.navigate(['dashboard']);

  }

  public goToPage(page: string): void {
    this.router.navigate([page]);
  }


}
