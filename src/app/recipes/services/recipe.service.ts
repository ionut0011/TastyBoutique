import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import {  RecipesModel, FiltersModel, IngredientModel, FilterModel } from '../models';
import { RecipessModel } from '../models/recipess.model';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {

  private endpoint: string = 'http://www.tastyboutique.tk:5341/api/v1/recipe';

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${JSON.parse(localStorage.getItem('userToken'))}`
    })
  };

  constructor(private readonly http: HttpClient) { }

  getAll(): Observable<RecipessModel> {
    return this.http.get<RecipessModel>(`${this.endpoint}`, this.httpOptions);
  }

  get(recipeId: string): Observable<RecipesModel> {
    return this.http.get<RecipesModel>(`${this.endpoint}/${recipeId}`, this.httpOptions);
  }

  getAllFilters(): Observable<FiltersModel> {
    return this.http.get<FiltersModel>(`http://www.tastyboutique.tk:5341/api/v1/filter`, this.httpOptions);
  }

  getIngredientByName(name: string): Observable<IngredientModel>
  {
    return this.http.get<IngredientModel>(`http://www.tastyboutique.tk:5341/api/v1/ingredient/${name}`, this.httpOptions);
  }

  getFilterByName(name: string): Observable<FilterModel>
  {
    return this.http.get<FilterModel>(`http://www.tastyboutique.tk:5341/api/v1/filter/${name}`, this.httpOptions);
  }
  addIngredient(name: string):Observable<any>{
    return this.http.post<any>(`http://www.tastyboutique.tk:5341/api/v1/ingredient`,name, this.httpOptions);
  }
  addFilter(name: string):Observable<any>{
    return this.http.post<any>(`http://www.tastyboutique.tk:5341/api/v1/filter`,name, this.httpOptions);
  }

  deleteRecipe(recipeId: string): Observable<RecipesModel> {
    return this.http.delete<RecipesModel>(`${this.endpoint}/${recipeId}`, this.httpOptions);
  }

  post(recipes: RecipesModel): Observable<any> {
    return this.http.post<any>(`${this.endpoint}`, recipes, this.httpOptions);
  }

  patch(recipes: RecipesModel): Observable<any> {
    return this.http.patch<any>(`${this.endpoint}/${recipes.id}`, recipes, this.httpOptions);
  }
}
