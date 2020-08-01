import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { SharedModule } from '../shared/shared.module';

import { RecipesRoutingModule } from './recipes-routing.module';
import { RecipesDetailsComponent } from './recipes-details/recipes-details.component';
import { RecipesListComponent } from './recipes-list/recipes-list.component';
<<<<<<< HEAD
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import {MatIconModule} from '@angular/material/icon'
=======

>>>>>>> e973232b96f6a9adb410106c265b1baec283cf65


@NgModule({
  declarations: [RecipesDetailsComponent, RecipesListComponent],
  imports: [
    CommonModule,
    RecipesRoutingModule,
    FormsModule,
<<<<<<< HEAD
    SharedModule,
    MatFormFieldModule,
    MatSelectModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatCardModule,
    MatIconModule

=======
    ReactiveFormsModule,
    SharedModule
>>>>>>> e973232b96f6a9adb410106c265b1baec283cf65
  ],
  exports:[
    RecipesDetailsComponent,
    RecipesListComponent

  ]
})
export class RecipesModule { }
