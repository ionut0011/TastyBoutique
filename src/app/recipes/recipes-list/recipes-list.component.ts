import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { RecipesModel, RecipessModel } from '../models';
import { RecipeService } from '../services/recipe.service';

@Component({
  selector: 'app-recipe-list',
  templateUrl: './recipes-list.component.html',
  styleUrls: ['./recipes-list.component.css'],
})
export class RecipesListComponent implements OnInit {
  public recipesList: RecipesModel[];

  constructor(
    private router: Router,
    private service: RecipeService) {}

  public ngOnInit(): void {

    this.service.getAll().subscribe((data: RecipessModel) => {
      this.recipesList = data.results;
    });

  }

  goToRecipe(id: string): void {
    console.log(id);
    this.router.navigate([`/recipes/details/${id}`]);
  }
}
