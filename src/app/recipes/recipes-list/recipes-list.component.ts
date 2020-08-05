import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RecipesGetModel,CollectionsModel } from '../models';
import { RecipeService } from '../services/recipe.service';
import { DomSanitizer } from '@angular/platform-browser';
import { Ng2ImgMaxService } from 'ng2-img-max';
import {CommentModel} from '../models/comment.model'


@Component({
  selector: 'app-recipe-list',
  templateUrl: './recipes-list.component.html',
  styleUrls: ['./recipes-list.component.css'],
})

export class RecipesListComponent implements OnInit {

  public recipeList: RecipesGetModel[];
  public collection: CollectionsModel={};
  public commentList: CommentModel;

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

      this.service.saveRecipes(this.recipeList);
      console.log("commentList", this.commentList);
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

  postCollections(id: string): void {
    this.collection.idRecipe=id;
    this.service.postCollections(this.collection).subscribe(data => {
      console.log(data);});
  }


  public goToPage(page: string): void {
    this.router.navigate([page]);
  }
}
