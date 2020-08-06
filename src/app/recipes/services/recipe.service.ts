import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RecipesModel, FiltersModel, IngredientModel, FilterModel } from '../models';
import { RecipesGetModel } from '../models/recipesget.model';


@Injectable({
  providedIn: 'root'
})
export class RecipeService {

  private endpoint: string = 'http://www.tastyboutique.tk:5341/api/v1/recipe';


  private recipes: RecipesGetModel[]=[];
  private httpOptions = {

    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${JSON.parse(localStorage.getItem('userToken'))}`
    })

  };

  constructor(private readonly http: HttpClient) { }

  getAll(): Observable<RecipesGetModel[]> {
    return this.http.get<RecipesGetModel[]>(`http://www.tastyboutique.tk:5341/api/v1/recipe`, this.httpOptions);
  }

  get(recipeId: string): Observable<RecipesGetModel> {
    return this.http.get<RecipesGetModel>(`${this.endpoint}/${recipeId}`, this.httpOptions);
  }

  post(recipes: RecipesModel): Observable<any> {
    return this.http.post<any>(`http://www.tastyboutique.tk:5341/api/v1/recipe`, recipes, this.httpOptions);
  }

  patch(recipes: RecipesModel): Observable<any> {
    return this.http.patch<any>(`${this.endpoint}/${recipes.id}`, recipes, this.httpOptions);
  }

  deleteRecipe(recipeId: string): Observable<RecipesModel> {
    return this.http.delete<RecipesModel>(`${this.endpoint}/${recipeId}`, this.httpOptions);
  }


  getAllFilters(): Observable<FiltersModel> {
    return this.http.get<FiltersModel>(`http://www.tastyboutique.tk:5341/api/v1/filter`, this.httpOptions);
  }
  addFilter(name: string):Observable<any>{
    return this.http.post<any>(`http://www.tastyboutique.tk:5341/api/v1/filter`,name, this.httpOptions);
  }

  getFilterByName(name: string): Observable<FilterModel>
  {
    return this.http.get<FilterModel>(`http://www.tastyboutique.tk:5341/api/v1/filter/${name}`, this.httpOptions);
  }


  getIngredientByName(name: string): Observable<IngredientModel>
  {
    return this.http.get<IngredientModel>(`http://www.tastyboutique.tk:5341/api/v1/ingredient/${name}`, this.httpOptions);
  }

  addIngredient(name: string):Observable<any>{
    return this.http.post<any>(`http://www.tastyboutique.tk:5341/api/v1/ingredient`,name, this.httpOptions);
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
