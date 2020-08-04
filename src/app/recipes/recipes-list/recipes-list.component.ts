import { Component, OnInit,OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { ViewChild, ElementRef } from '@angular/core'
import { RecipesModel, RecipesGetModel } from '../models';
import { RecipeService } from '../services/recipe.service';
import { Subscription } from 'rxjs';
import { DomSanitizer } from '@angular/platform-browser';
import { UserService } from 'src/app/shared/services';
@Component({
  selector: 'app-recipe-list',
  templateUrl: './recipes-list.component.html',
  styleUrls: ['./recipes-list.component.css'],
})

export class RecipesListComponent implements OnInit {
  public recipeList: RecipesGetModel[];
  private imageList: any = [];

  constructor(
    private router: Router,
    private service: RecipeService,
    private domSanitizer: DomSanitizer) {}

  public ngOnInit(): void {

    if(this.service.getRecipes().length == 0){
    this.service.getAll().subscribe((data: RecipesGetModel[]) => {
      this.recipeList = data;
      this.recipeList.forEach(element => {
        if(element.image.length>5){
        let link:any = 'data:image/png;base64,'+element.image;
        element.image = link;
        }
      });
      console.log(data);
      this.service.saveRecipes(this.recipeList);
    });
  }
  else
    this.recipeList = this.service.getRecipes();

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
