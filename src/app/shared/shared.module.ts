import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TileComponent } from './tile/tile.component';
import { HeaderComponent } from './header/header.component';
import { MatIconModule } from '@angular/material/icon';

import {SubmitButtonComponent} from './submit-button/submit-button.component';
import { NavbarComponent } from './navbar/navbar.component'

@NgModule({
  declarations: [TileComponent, HeaderComponent, SubmitButtonComponent, NavbarComponent],
  imports: [CommonModule, MatIconModule],
  exports: [TileComponent, HeaderComponent, SubmitButtonComponent,NavbarComponent],
})
export class SharedModule { }
