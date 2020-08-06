import { Component, OnInit,OnDestroy } from '@angular/core';
import { Router } from '@angular/router';

import { FilterModel,FiltersModel, IngredientModel } from '../models';
import { ViewChild, ElementRef } from '@angular/core'
import { RecipesModel, RecipesGetModel,CollectionsModel } from '../models';

import { RecipeService } from '../services/recipe.service';
import { DomSanitizer } from '@angular/platform-browser';

import { Ng2ImgMaxService } from 'ng2-img-max';

import { UserService } from 'src/app/shared/services';
import {CommentModel} from '../models/comment.model'
import { FormControl } from '@angular/forms';


@Component({
  selector: 'app-recipe-list',
  templateUrl: './recipes-list.component.html',
  styleUrls: ['./recipes-list.component.css'],
})

export class RecipesListComponent implements OnInit {
  public recipeList: RecipesGetModel[];
  filterssList:FiltersModel;
  public collection: CollectionsModel={};
  private imageList: any = [];
  filter:FormControl=new FormControl();


  public commentList: CommentModel;

  constructor(
    private router: Router,
    private service: RecipeService,
    private domSanitizer: DomSanitizer,
    private ng2ImgMax: Ng2ImgMaxService) {}


  public ngOnInit(): void {

    this.service.getAllFilters().subscribe((data: FiltersModel) => {
      this.filterssList = data;});

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


  postCollections(id: string): void {

    this.collection.idRecipe=id;
    this.service.postCollections(this.collection).subscribe(data => {
      console.log(data);});
  }

  public goToPage(page: string): void {
    this.router.navigate([page]);
  }

  SearchFilter():void
  {
    this.service.searchFilters(this.filter.value).subscribe(data => {
      this.recipeList = data;
      console.log(data);});

  }

  SearchIngredients():void
  {
    this.service.searchIngredients(this.filter.value).subscribe(data => {
      this.recipeList = data;
      console.log(data);});

  }

}
