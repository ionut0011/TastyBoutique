import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TileComponent } from './tile/tile.component';
import { HeaderComponent } from './header/header.component';
import { MatIconModule } from '@angular/material/icon'



@NgModule({
  declarations: [TileComponent, HeaderComponent],
  imports: [CommonModule, MatIconModule],
  exports: [TileComponent, HeaderComponent],
})
export class SharedModule { }
