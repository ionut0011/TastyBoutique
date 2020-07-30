import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  OnInit,
} from '@angular/core';
import { Router } from '@angular/router';

import { UserService } from '../services/user.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})

export class HeaderComponent {
  public username: string;
  constructor( private readonly router: Router,
    private readonly cdRef: ChangeDetectorRef,
    public readonly userService: UserService) { }


  public logout(): void {
    this.router.navigate(['authentication']);
    localStorage.removeItem('email');
    this.username = localStorage.getItem('email');
  }

}
