import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TileComponent } from './tile/tile.component';
import { HeaderComponent } from './header/header.component';
import { MatIconModule } from '@angular/material/icon';
import { StarsReviewComponent } from './stars-review/stars-review.component';
import {SubmitButtonComponent} from './submit-button/submit-button.component';

@NgModule({
  declarations: [TileComponent, HeaderComponent, StarsReviewComponent, SubmitButtonComponent],
  imports: [CommonModule, MatIconModule],
  exports: [TileComponent, HeaderComponent,StarsReviewComponent, SubmitButtonComponent],
})
export class SharedModule { }
