import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RecipeService } from '../services/recipe.service';
import { CollectionsService } from '../services/collections.service';
import { RecipesGetModel, CollectionsModel } from '../models';
import {ToastrService} from 'ngx-toastr'

@Component({
  selector: 'app-recipes-saved',
  templateUrl: './recipes-saved.component.html',
  styleUrls: ['./recipes-saved.component.css']
})
export class RecipesSavedComponent implements OnInit {
  public recipeList: RecipesGetModel[];
  public collection: CollectionsModel={};
  private deleted: boolean = true;

  constructor( private router: Router,
    private service: RecipeService,
    private toastr: ToastrService,
    private serviceCollections: CollectionsService,
    ) { }

  ngOnInit(): void {
    this.serviceCollections.getAllCollections().subscribe((data: RecipesGetModel[]) => {
      this.recipeList = data;
      this.recipeList.forEach(element => {
        if(element.image.length>5){
          let link:any = 'data:image/png;base64,'+element.image;
          element.image = link;
        }
      });
    });
  }

  goToRecipe(id: string): void {
    this.router.navigate([`/recipes/details/${id}`]);
  }

  public deleteRecipeCollection(id:string): void{

    this.collection.idRecipe=id;
    this.serviceCollections.deleteRecipeCollection(this.collection).subscribe(data => {
      console.log(data);
        this.deleted = true;
        this.toastr.success('Deleted');
    },
    (error)=>{
      this.toastr.error('Something went wrong.')
    });
  }

  public goToPage(page: string): void {
    this.router.navigate([page]);
  }
}
