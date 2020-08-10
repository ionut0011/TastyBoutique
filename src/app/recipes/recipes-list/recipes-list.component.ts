import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RecipesGetModel,CollectionsModel } from '../models';
import { RecipeService } from '../services/recipe.service';
import { DomSanitizer } from '@angular/platform-browser';
import { Ng2ImgMaxService } from 'ng2-img-max';
import {CommentModel} from '../models/comment.model'
import {RecipesModel} from '../../recipes/models/recipes.model'
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import {ToastrService} from 'ngx-toastr'

import { FiltersModel } from '../models';

import { CollectionsService } from '../services/collections.service';
import { SearchService } from '../services/search.service';



@Component({
  selector: 'app-recipe-list',
  templateUrl: './recipes-list.component.html',
  styleUrls: ['./recipes-list.component.css'],
})

export class RecipesListComponent implements OnInit {

  public recipeList: RecipesGetModel[];
  filterssList:FiltersModel;
  public collection: CollectionsModel={};
  public recipeeList: RecipesGetModel[];
  public addedToCollection : boolean = true;
  public deletedRecipe : boolean = false;
  private imageList: any = [];
  filter:FormControl=new FormControl();
  ingredientsList:FormControl=new FormControl([]);

  public commentList: CommentModel;
  public form : FormGroup;

  constructor(private toastr: ToastrService,
    private router: Router,
    private service: RecipeService,
    private serviceCollections: CollectionsService,
    private serviceSearch: SearchService,
    private domSanitizer: DomSanitizer,
    private ng2ImgMax: Ng2ImgMaxService) {

    }


  public ngOnInit(): void {

    this.service.getAllFilters().subscribe((data: FiltersModel) => {
      this.filterssList = data;});

    this.service.getAll().subscribe((data: RecipesGetModel[]) => {
      this.recipeList = data;
      console.log("RECIPELIST", this.recipeList);
      console.log(this.recipeeList);


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
    console.log(this.recipeList);
    console.log(id);
          this.service.deleteRecipe(id).subscribe(data => {
            this.deletedRecipe = true;
            this.toastr.success("Deleted");
        },
        (error)=>{
          this.toastr.error("Could not delete")
        });
}

  postCollections(id: string): void {
    this.collection.idRecipe=id;
    this.serviceCollections.postCollections(this.collection).subscribe(data => {
      console.log(data);
      this.addedToCollection = true;
      this.toastr.success('Added to favorite list.')

    },
    (error)=>{
      this.toastr.error('Something went wrong')
    });


  }


  public goToPage(page: string): void {
    this.router.navigate([page]);
  }

  SearchFilter():void
  {
    this.serviceSearch.searchFilters(this.filter.value).subscribe(data => {
      this.recipeList = data;
      console.log(data);
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

  SearchIngredients():void
  {
    this.serviceSearch.searchIngredients(this.ingredientsList.value).subscribe(data => {
      this.recipeList = data;
      console.log(data);
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

}
