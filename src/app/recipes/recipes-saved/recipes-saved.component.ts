import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RecipeService } from '../services/recipe.service';
import { RecipesGetModel, CollectionsModel } from '../models';
@Component({
  selector: 'app-recipes-saved',
  templateUrl: './recipes-saved.component.html',
  styleUrls: ['./recipes-saved.component.css']
})
export class RecipesSavedComponent implements OnInit {
  public recipeList: RecipesGetModel[];
  public collection: CollectionsModel={};

  constructor( private router: Router,
    private service: RecipeService,) { }

  ngOnInit(): void {

      this.service.getAllCollections().subscribe((data: RecipesGetModel[]) => {
        this.recipeList = data;
        this.recipeList.forEach(element => {
          if(element.image.length>5){
          let link:any = 'data:image/png;base64,'+element.image;
          element.image = link;
          }
        });
        console.log(data);
      });

  }

  goToRecipe(id: string): void {
    this.router.navigate([`/recipes/details/${id}`]);
  }


  public deleteRecipeCollection(id:string): void{

    this.collection.idRecipe=id;
    this.service.deleteRecipeCollection(this.collection).subscribe(data => {
      console.log(data);});
    window.location.reload();


  }

  public goToPage(page: string): void {
    this.router.navigate([page]);
  }


}
