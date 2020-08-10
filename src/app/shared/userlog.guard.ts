import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable} from 'rxjs';
import {Router} from '@angular/router';
import { map} from 'rxjs/operators';
import { UserService } from '../shared/services/user.service';


@Injectable({
  providedIn: 'root'
})
export class UserlogGuard implements CanActivate {

  constructor( private readonly userService:UserService,
    private router: Router) {
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
      return !!sessionStorage.getItem('userToken');
  }



}
