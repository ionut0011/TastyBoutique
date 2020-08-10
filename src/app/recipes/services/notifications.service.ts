import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { RecipesModel, RecipesGetModel} from '../models';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class NotificationsService {
  private endpoint: string = 'http://www.tastyboutique.tk:5341/api/v1/notifications';
  private httpOptions = {

    headers: new HttpHeaders({
      'Content-Type': 'application/json',

      'Authorization': `Bearer ${JSON.parse(localStorage.getItem('userToken'))}`
    })

  };

  constructor(private readonly http: HttpClient) { }

  getNotification(): Observable<RecipesGetModel> {
    return this.http.get<RecipesGetModel>(`${this.endpoint}`, this.httpOptions);
  }

  patchNotification(idRecipe: string): Observable<any>{
    return this.http.patch<any>(`${this.endpoint}/${idRecipe}`, idRecipe, this.httpOptions);
  }

}
