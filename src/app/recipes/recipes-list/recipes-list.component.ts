import { Component, OnInit,OnDestroy } from '@angular/core';
import { Router } from '@angular/router';

import { RecipesModel, RecipessModel } from '../models';
import { RecipeService } from '../services/recipe.service';
import { Subscription } from 'rxjs';
@Component({
  selector: 'app-recipe-list',
  templateUrl: './recipes-list.component.html',
  styleUrls: ['./recipes-list.component.css'],
})
export class RecipesListComponent implements OnInit {
  public recipeList: RecipessModel;


  constructor(
    private router: Router,
    private service: RecipeService) {}

  public ngOnInit(): void {

    this.service.getAll().subscribe((data: RecipessModel) => {
      this.recipeList = data;
      console.log(data);
    });

  }

  goToRecipe(id: string): void {
    this.router.navigate([`/recipes/details/${id}`]);
  }



  public DeleteRecipe(id:string): void{

    this.service.deleteRecipe(id).subscribe(data => {
      console.log(data);});
    window.location.reload();

  }



}
