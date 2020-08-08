import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { RecipesModel} from '../models';
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

  getNotification(): Observable<RecipesModel> {
    return this.http.get<any>(`${this.endpoint}`, this.httpOptions);
  }

  patchNotification(recipeId: string): Observable<any>{
    return this.http.patch<any>(`${this.endpoint}/${recipeId}`, this.httpOptions);
  }

}
