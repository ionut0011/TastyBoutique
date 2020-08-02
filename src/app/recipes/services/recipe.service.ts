import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import {  RecipesModel, FiltersModel } from '../models';
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



  post(recipes: RecipesModel): Observable<any> {
    return this.http.post<any>(`${this.endpoint}`, recipes, this.httpOptions);
  }

  patch(recipes: RecipesModel): Observable<any> {
    return this.http.patch<any>(`${this.endpoint}/${recipes.id}`, recipes, this.httpOptions);
  }
}
