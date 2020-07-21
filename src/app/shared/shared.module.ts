import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TileComponent } from './tile/tile.component';
import { HeaderComponent } from './header/header.component';



@NgModule({
  declarations: [TileComponent, HeaderComponent],
  imports: [
    CommonModule
  ],
  exports:[
      TileComponent,
      HeaderComponent
  ]
})
export class SharedModule { }
