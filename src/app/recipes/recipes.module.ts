import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { SharedModule } from '../shared/shared.module';

import { RecipesRoutingModule } from './recipes-routing.module';
import { RecipesDetailsComponent } from './recipes-details/recipes-details.component';
import { RecipesListComponent } from './recipes-list/recipes-list.component';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';


@NgModule({
  declarations: [RecipesDetailsComponent, RecipesListComponent],
  imports: [
    CommonModule,
    RecipesRoutingModule,
    FormsModule,
    SharedModule,
    MatFormFieldModule,
    MatSelectModule,
    ReactiveFormsModule
  ],
  exports:[
    RecipesDetailsComponent,
    RecipesListComponent

  ]
})
export class RecipesModule { }