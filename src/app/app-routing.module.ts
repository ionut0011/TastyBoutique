import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { RecipesDetailsComponent } from './recipes/recipes-details/recipes-details.component';
import { RecipesListComponent } from './recipes/recipes-list/recipes-list.component';

const routes: Routes = [

{
  path: '',
  pathMatch: 'full',
  redirectTo: 'login',
},
{
  path:'login',
  loadChildren: () => import('./login/login.module').then(m=>m.LoginModule)
},
{
  path:'dashboard',
  loadChildren: () => import('./dashboard/dashboard.module').then(m=>m.DashboardModule)
},
{
  path: 'notifications',
  loadChildren: () =>
    import('./notifications/notifications.module').then(
      (m) => m.NotificationsModule
    ),
},
{ path: 'list', component: RecipesListComponent },
{ path: 'create-recipe', component: RecipesDetailsComponent },
{
  path: 'recipes',
  loadChildren: () => import('./recipes/recipes.module').then((m) => m.RecipesModule),
},
];

@NgModule({
  imports: [RouterModule.forRoot(routes),],
  exports: [RouterModule]
})
export class AppRoutingModule { }
