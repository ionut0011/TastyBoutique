import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthentificationService {

  public endpoint: string =

    'http://www.tastyboutique.tk/api/v1/auth';
  constructor(private readonly httpClient: HttpClient) {}


  public register(data: unknown): Observable<unknown> {
    return this.httpClient.post(`${this.endpoint}/register`, data);
  }

  public login(data: unknown): Observable<unknown> {
    return this.httpClient.post(`${this.endpoint}/login`, data);
  }

  public recover(data: unknown): Observable<unknown> {
    return this.httpClient.post(`${this.endpoint}/recover`, data);
  }

}
