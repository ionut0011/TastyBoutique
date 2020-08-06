import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import{CommentModel} from '../models/comment.model';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class CommentsService {
  private endpoint: string = 'http://www.tastyboutique.tk:5341/api/v1/recipe';



  private httpOptions = {

    headers: new HttpHeaders({
      'Content-Type': 'application/json',

      'Authorization': `Bearer ${JSON.parse(localStorage.getItem('userToken'))}`
    })

  };

  constructor(private readonly http: HttpClient) { }

  getComments(recipeId: string):Observable<any>{
    return this.http.get<any>(`${this.endpoint}/${recipeId}/comments`, this.httpOptions);
  }

  addComment(recipeId: string, comment: CommentModel) :Observable<any>{
    return this.http.post<any>(`${this.endpoint}/${recipeId}/comments`,comment,this.httpOptions);
  }

  deleteComment(recipeId: string, commentId: string) :Observable<CommentModel>{
    return this.http.delete<CommentModel>(`${this.endpoint}/${recipeId}/${commentId}`, this.httpOptions);
  }
}
