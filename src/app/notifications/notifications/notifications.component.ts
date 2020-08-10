import { Component, OnInit } from '@angular/core';
import {NotificationsService} from '../../recipes/services/notifications.service';
import { Subscription } from 'rxjs';
import { RecipesModel, RecipesGetModel} from '../../../app/recipes/models';
import { Router } from '@angular/router';
import {ToastrService} from 'ngx-toastr'
import { HttpClient, HttpHeaders } from '@angular/common/http';



@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.css']
})
export class NotificationsComponent implements OnInit {

  private routeSub: Subscription = new Subscription();
  public notificationList : RecipesGetModel;


  constructor(
    private toastr: ToastrService,
    private readonly http :HttpClient,


    private service: NotificationsService,
    private router: Router,

  ) { }

  ngOnInit(): void {
    this.service.getNotification().subscribe((data : RecipesGetModel)=>{
      this.notificationList = data;
      console.log(this.notificationList);
    })
  }

  patchNotification(recipeId: string):void{
    this.service.patchNotification(recipeId).subscribe(data =>{
      console.log("e ok");
      this.toastr.success("Now you can read the recipe");
      this.goToRecipe(recipeId);
    },
    (error=>{
      this.toastr.error("Something went wrong");
    }));

  }

  public goToPage(page: string): void {
    this.router.navigate([page]);
  }


  goToRecipe(id: string): void {
    this.router.navigate([`/recipes/details/${id}`]);
  }

}
