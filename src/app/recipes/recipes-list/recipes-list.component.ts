import { Component, OnInit,OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import {CommentModel} from '../models/comment.model'
import { RecipesGetModel } from '../models';
import { RecipeService } from '../services/recipe.service';

@Component({
  selector: 'app-recipe-list',
  templateUrl: './recipes-list.component.html',
  styleUrls: ['./recipes-list.component.css'],
})
export class RecipesListComponent implements OnInit {
  public recipeList: RecipesGetModel[];
  public commentList: CommentModel;
  constructor(
    private router: Router,
    private service: RecipeService) {}

  public ngOnInit(): void {

    this.service.getAll().subscribe((data: RecipesGetModel[]) => {
      this.recipeList = data;
      this.recipeList.forEach(element => {
        var link:any = "data:image/jpg;base64,"+element.image;
        element.image = link;
      });
      console.log(data);
      console.log("commentList", this.commentList)
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
