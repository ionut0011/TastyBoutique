import { Component, OnInit,OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { RecipesModel, RecipesGetModel } from '../models';
import { RecipeService } from '../services/recipe.service';
import { DomSanitizer } from '@angular/platform-browser';
import { Ng2ImgMaxService } from 'ng2-img-max';
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
    private domSanitizer: DomSanitizer,
    private ng2ImgMax: Ng2ImgMaxService) {}


  public ngOnInit(): void {

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


  goToRecipe(id: string): void {
    this.router.navigate([`/recipes/details/${id}`]);
  }

  public DeleteRecipe(id:string): void{

    this.service.deleteRecipe(id).subscribe(data => {
      console.log(data);});


  }

}
