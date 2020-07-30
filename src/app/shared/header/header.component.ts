import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  OnInit,
} from '@angular/core';
import { Router } from '@angular/router';

import { UserService } from '../user.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})

export class HeaderComponent implements OnInit{
  public username: string;
  constructor( private readonly router: Router,

    public readonly userService: UserService) { }


  logout(): void {

    this.router.navigate(['login']);
    this.userService.email.next(null);
    console.log("clicked logout");

  }
  ngOnInit(): void{
      this.username='';
  }

}





