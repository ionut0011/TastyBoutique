import { Component} from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent  {

  constructor(private readonly router: Router ) {}

  public goToPage(page: string): void {
    this.router.navigate([page]);
  }

  public goToPageList(page: string): void {

    if(page == this.router.url.split('/')[1])
      window.location.reload()
    else
      this.router.navigate([page]);
    }
  }


