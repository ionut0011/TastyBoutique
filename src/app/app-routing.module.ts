import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RecipesDetailsComponent } from './recipes/recipes-details/recipes-details.component';
import { RecipesListComponent } from './recipes/recipes-list/recipes-list.component';
import { LoginComponent } from './login/login/login.component';
import { RegisterComponent } from './login/register/register.component';
import { RecoverComponent } from './login/recover/recover.component';
import { RecipesSavedComponent } from './recipes/recipes-saved/recipes-saved.component';
import { UserlogGuard} from './shared/userlog.guard';

const routes: Routes = [

{
  path: '',
  pathMatch: 'full',
  redirectTo: 'login',
},
{
  path:'login2',
  loadChildren: () => import('./login/login.module').then(m=>m.LoginModule)
},
{ path: 'register', component: RegisterComponent },
{ path: 'recover', component: RecoverComponent },
{ path: 'login', component: LoginComponent },
{
  path:'dashboard',
  loadChildren: () => import('./dashboard/dashboard.module').then(m=>m.DashboardModule),
  canActivate: [UserlogGuard]
},
{
  path: 'notifications',
  loadChildren: () =>import('./notifications/notifications.module').then((m) => m.NotificationsModule),
  canActivate: [UserlogGuard]
},

{
  path: 'recipes',
  loadChildren: () => import('./recipes/recipes.module').then((m) => m.RecipesModule),
  canActivate: [UserlogGuard]
},
{ path: 'list', component: RecipesListComponent,
  canActivate: [UserlogGuard] },
{ path: 'create-recipe', component: RecipesDetailsComponent,
canActivate: [UserlogGuard] },
{ path: 'collections', component: RecipesSavedComponent,
canActivate: [UserlogGuard] },

];

@NgModule({
  imports: [RouterModule.forRoot(routes,{useHash:true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
