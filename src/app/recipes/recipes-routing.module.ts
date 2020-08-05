import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { RecipesDetailsComponent } from './recipes-details/recipes-details.component';
import { RecipesListComponent } from './recipes-list/recipes-list.component';
import { RecipesSavedComponent } from './recipes-saved/recipes-saved.component';



const routes: Routes = [
  {
    path: 'list',
    pathMatch: 'full',
    component: RecipesListComponent,
  },
  {
    path: 'details/:id',
    pathMatch: 'full',
    component: RecipesDetailsComponent,
  },
  {
    path: 'collections',
    pathMatch: 'full',
    component: RecipesSavedComponent,
  },



];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RecipesRoutingModule { }
