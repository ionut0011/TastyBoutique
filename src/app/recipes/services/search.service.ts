import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RecipesGetModel } from '../models/recipesget.model';
@Injectable({
  providedIn: 'root'
})
export class SearchService {

  private endpoint: string = 'http://www.tastyboutique.tk:5341/api/v1/search';

  private recipes: RecipesGetModel[]=[];
  private httpOptions = {

    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${JSON.parse(sessionStorage.getItem('userToken'))}`
    })

  };

  constructor(private readonly http: HttpClient) { }

  searchIngredients(ingredientsList:string[]): Observable<any> {
    return this.http.get<RecipesGetModel[]>(`${this.endpoint}`,{headers:this.httpOptions.headers, params: {'query' : ingredientsList}});
  }

  s
  searchFilters(filter:string): Observable<RecipesGetModel[]> {
    return this.http.get<RecipesGetModel[]>(`${this.endpoint}/${filter}`,this.httpOptions);
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
