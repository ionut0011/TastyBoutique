import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RecipesGetModel,CollectionsModel } from '../models';
import { RecipeService } from '../services/recipe.service';
import {CommentModel} from '../models/comment.model'
import { FormControl, FormGroup } from '@angular/forms';
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
  public addedToCollection : boolean = true;
  public deletedRecipe : boolean = false;
  filter:FormControl=new FormControl();
  ingredientsList:FormControl=new FormControl([]);
  ingredientsList2:FormControl=new FormControl([]);
  public commentList: CommentModel;
  public form : FormGroup;

  constructor(private toastr: ToastrService,
    private router: Router,
    private service: RecipeService,
    private serviceCollections: CollectionsService,
    private serviceSearch: SearchService) {}

  public ngOnInit(): void {
    this.service.getAllFilters().subscribe((data: FiltersModel) => {
      this.filterssList = data;
    });
    this.getAllRecipes();
  }

  public getAllRecipes(): void{
    this.service.getAll().subscribe((data: RecipesGetModel[]) => {
      this.recipeList = data;
      this.recipeList.forEach(element => {
        if(element.image.length>5){
        let link:any = 'data:image/png;base64,'+element.image;
        element.image = link;
        }
      });
      this.service.saveRecipes(this.recipeList);
    });
  }

  goToRecipe(id: string): void {
    this.router.navigate([`/recipes/details/${id}`]);
  }

  public deleteRecipe(id:string): void{
    this.service.deleteRecipe(id).subscribe(data => {
  },
  (error) =>{
    console.log(error.error.text)
    if(error.error.text == "Deleted")
    {
      this.deletedRecipe = true;
      this.toastr.success("Deleted");
      this.getAllRecipes();
    }
    else
      this.toastr.error('You can\'t delete this recipe.');
    });
  }

  postCollections(id: string): void {
    this.collection.idRecipe=id;
    this.serviceCollections.postCollections(this.collection).subscribe(data => {
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

  searchFilter():void
  {
    this.serviceSearch.searchFilters(this.filter.value).subscribe(data => {
      this.recipeList = data;
      this.recipeList.forEach(element => {
        if(element.image.length>5){
        let link:any = 'data:image/png;base64,'+element.image;
        element.image = link;
        }
      });
      this.service.saveRecipes(this.recipeList);
    });
  }

  searchIngredients():void
  {
    let splitted= this.ingredientsList.value.split(",");
      splitted.forEach(element => {
        this.ingredientsList2.value.push(element);
      });
      console.log(this.ingredientsList2);

    this.serviceSearch.searchIngredients(splitted).subscribe(data=> {
      this.recipeList =data;
      console.log(this.recipeList);
      this.recipeList.forEach(element => {
        if(element.image.length>5){
        let link:any = 'data:image/png;base64,'+element.image;
        element.image = link;
        }
      });
      this.service.saveRecipes(this.recipeList);
    });
  }
}
