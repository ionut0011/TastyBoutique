import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(private readonly router: Router ) {
    }

  ngOnInit(): void {
  }

  public goToPage(page: string): void {
    this.router.navigate([page]);
  }

  public goToPageList(page: string): void {

    this.router.navigate([page]);

  }

}
