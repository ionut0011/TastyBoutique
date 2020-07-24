import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { RecipesDetailsComponent } from './recipes-details/recipes-details.component';
import { RecipesListComponent } from './recipes-list/recipes-list.component';



const routes: Routes = [
  {
    path: 'list',
    pathMatch: 'full',
    component: RecipesListComponent,
  },
  {
    path: 'details',
    pathMatch: 'full',
    component: RecipesDetailsComponent,
  },


];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RecipesRoutingModule { }
