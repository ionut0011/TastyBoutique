import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TileComponent } from './tile/tile.component';
import { HeaderComponent } from './header/header.component';
import { MatIconModule } from '@angular/material/icon';
import { NavbarComponent } from './navbar/navbar.component'

@NgModule({
  declarations: [TileComponent, HeaderComponent, NavbarComponent],
  imports: [CommonModule, MatIconModule],
  exports: [TileComponent, HeaderComponent,NavbarComponent],
})
export class SharedModule { }
