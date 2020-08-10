import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CollectionsModel } from '../models/collections.model';
import { Observable } from 'rxjs';
import { RecipesGetModel } from '../models/recipesget.model';

@Injectable({
  providedIn: 'root'
})
export class CollectionsService {

  private endpoint: string = 'http://www.tastyboutique.tk:5341/api/v1/collections';
  private recipes: RecipesGetModel[]=[];
  private httpOptions = {

    headers: new HttpHeaders({
      'Content-Type': 'application/json',

      'Authorization': `Bearer ${JSON.parse(sessionStorage.getItem('userToken'))}`
    })

  };

  constructor(private readonly http: HttpClient) { }


  postCollections(recipes:CollectionsModel): Observable<any> {
    return this.http.post<any>(`${this.endpoint}`, recipes, this.httpOptions);
  }

  getAllCollections(): Observable<RecipesGetModel[]> {
    return this.http.get<RecipesGetModel[]>(`${this.endpoint}`, this.httpOptions);
  }

  deleteRecipeCollection(recipeId: CollectionsModel): Observable<any> {
    console.log(recipeId);
    return this.http.delete<any>(`${this.endpoint}/${recipeId.idRecipe}`, this.httpOptions);
  }

  saveRecipes(recipes :RecipesGetModel[])
  {
    this.recipes = recipes;
  }

  getRecipes()
  {
    return this.recipes;
  }
}
