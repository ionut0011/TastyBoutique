import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';

import { RecipesRoutingModule } from './recipes-routing.module';
import { RecipesDetailsComponent } from './recipes-details/recipes-details.component';
import { RecipesListComponent } from './recipes-list/recipes-list.component';


@NgModule({
  declarations: [RecipesDetailsComponent, RecipesListComponent],
  imports: [
    CommonModule,
    RecipesRoutingModule,
    FormsModule,
    SharedModule
  ],
  exports:[
    RecipesDetailsComponent,
    RecipesListComponent

  ]
})
export class RecipesModule { }
