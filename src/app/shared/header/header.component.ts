import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  OnInit,
} from '@angular/core';
import { Router } from '@angular/router';


import { UserService } from '../services';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})

export class HeaderComponent{
  public username: string;
  constructor(
    private readonly router: Router,
    private readonly cdRef: ChangeDetectorRef,
    public readonly userService: UserService) { }

    public ngOnInit(): void {

      let user = localStorage.getItem('email');
      if(user!=null)
      {
        setTimeout(() => this.userService.username.next(user.substring(1,user.length-1).split('@')[0]), 0);
      }
  }

  public logout(): void {

    this.router.navigate(['login']);
    localStorage.removeItem('userToken');
    this.username = localStorage.getItem('email');
    localStorage.removeItem('email');

  }

}





