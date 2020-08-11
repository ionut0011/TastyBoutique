import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { RecipesRoutingModule } from './recipes-routing.module';
import { RecipesDetailsComponent } from './recipes-details/recipes-details.component';
import { RecipesListComponent } from './recipes-list/recipes-list.component';
import { RecipesSavedComponent } from './recipes-saved/recipes-saved.component';



@NgModule({
  declarations: [RecipesDetailsComponent, RecipesListComponent, RecipesSavedComponent],
  imports: [
    CommonModule,
    RecipesRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule
  ],
  exports:[
    RecipesDetailsComponent,
    RecipesListComponent

  ]
})
export class RecipesModule { }
