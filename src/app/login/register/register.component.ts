import { Component, OnInit,OnDestroy } from '@angular/core';
import {Router} from '@angular/router'
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent implements OnInit,OnDestroy {

  public username:string=null;
  public email:string=null;
  public password:string=null;
  public fullname:string=null;
  public age:number=null;

  isAdmin=true;


  constructor(private router: Router) { }



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

  public goToPage(page: string): void {
    this.router.navigate([page]);
  }


}

