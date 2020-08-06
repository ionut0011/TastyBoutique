import { Component, OnInit, OnDestroy } from '@angular/core';
import {Router} from '@angular/router';
import { AuthentificationService } from '../services/authentification.service';
import { RecoverModel } from '../models/recover.model';
import { UserService } from '../../shared/services';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import {ToastrService} from 'ngx-toastr'

@Component({
  selector: 'app-recover',
  templateUrl: './recover.component.html',
  styleUrls: ['./recover.component.css']
})

export class RecoverComponent implements OnInit,OnDestroy {

  public formGroup: FormGroup;
  public email:string=null;
  public newPassword:string=null;


  constructor(
    private toastr: ToastrService,
    private readonly authentificationService:AuthentificationService,
    private router: Router,
    private readonly userService:UserService,
    private readonly formBuilder: FormBuilder
    ) {
      this.formGroup = this.formBuilder.group({
        email: new FormControl(null),
        newPassword: new FormControl(null),
      });
      this.userService.username.next('');

     }


  ngOnInit(): void {
  }

  ngOnDestroy():void
  {
    console.log("clicked recover");
  }

  clickedLogin():void{


    console.log('s-a apasat change password');
    const data: RecoverModel = this.formGroup.getRawValue();
    this.authentificationService.recover(data).subscribe(() => {
      this.router.navigate(['login']);
      this.toastr.success("Successfully changed password");
    },
    (error)=>{
      this.toastr.error("Something went wrong");
    });
  }

  public goToPage(page: string): void {
    this.router.navigate([page]);
  }

}

