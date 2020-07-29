import { Component, OnInit, OnDestroy } from '@angular/core';
import {Router} from '@angular/router'


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit,OnDestroy {

  public username:string=null;
  public email:string=null;
  public password:string=null;
  public fullname:string=null;
  public age:number=null;

  isSetRegistered = false;
  isAdmin = false;

  constructor(private router: Router) { }

  setRegister(): void {
    this.isSetRegistered = !this.isSetRegistered;
  }

  setAdmin(): void {
    this.isAdmin = !this.isAdmin;
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


}
