import { Component, OnInit,OnDestroy } from '@angular/core';
import { Router } from '@angular/router';

import { FiltersModel } from '../models';
import { RecipesGetModel,CollectionsModel } from '../models';

import { RecipeService} from '../services/recipe.service';
import { CollectionsService } from '../services/collections.service';
import { SearchService } from '../services/search.service';
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
    private serviceCollections: CollectionsService,
    private serviceSearch: SearchService,
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
    this.serviceCollections.postCollections(this.collection).subscribe(data => {
      console.log(data);});
  }

  public goToPage(page: string): void {
    this.router.navigate([page]);
  }

  SearchFilter():void
  {
    this.serviceSearch.searchFilters(this.filter.value).subscribe(data => {
      this.recipeList = data;
      console.log(data);});

  }

  SearchIngredients():void
  {
    this.serviceSearch.searchIngredients(this.filter.value).subscribe(data => {
      this.recipeList = data;
      console.log(data);});

  }

}
