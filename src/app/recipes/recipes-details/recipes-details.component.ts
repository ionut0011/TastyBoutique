import { Component, OnInit,OnDestroy} from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';

import { RecipesModel } from '../models';
import { RecipeService } from '../services/recipe.service';

@Component({
  selector: 'app-recipes-details',
  templateUrl: './recipes-details.component.html',
  styleUrls: ['./recipes-details.component.css']
})
export class RecipesDetailsComponent implements OnInit,OnDestroy
{


  fileToUpload: any;
  imageUrl: any;

  formGroup: FormGroup;

  isAdmin: boolean;
  isAddMode: boolean;
  photos: Blob[] = [];

  private routeSub: Subscription = new Subscription();

  get description(): string {
    return this.formGroup.get('description').value;
  }

  get isFormDisabled(): boolean {
    return this.formGroup.disabled;
  }


  name: string;



  filtersList: string[] = ['Gluten free', 'Vegan', 'Sugar free'];
  ingredientsList:string[] =[];
  foodordrink:string[] =[];
  type1:string;
  typees:number;
  typeesList: string[] = ['Food', 'Drink'];




  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private service: RecipeService
    ) { }

  ngOnInit(): void {

    this.formGroup = this.formBuilder.group({
      id: new FormControl(),
      title: new FormControl(),
      description: new FormControl(),
      private: new FormControl(false)
    })


    if (this.router.url === '/create-recipe') {
      this.isAddMode = true;
    } else {
      //Getting id from url
      this.routeSub = this.activatedRoute.params.subscribe(params => {
        //Getting details for the trip with the id found
        this.service.get(params['id']).subscribe((data: RecipesModel) => {
          this.formGroup.patchValue(data);
        })
        this.formGroup.disable();
      });
      this.isAddMode = false;
    }
    this.isAdmin = true;


  }

  ngOnDestroy(): void{
    this.routeSub.unsubscribe();

  }

  startUpdating() {
    this.formGroup.enable();
  }

  save() {
    if (this.isAddMode) {
      this.service.post(this.formGroup.getRawValue()).subscribe();
      this.router.navigate(['list']);
    } else {
      this.service.patch(this.formGroup.getRawValue()).subscribe();
    }


    this.photos.push(this.imageUrl);
    this.imageUrl = null;
    this.formGroup.disable();
  }

  handleFileInput(file: FileList) {
    this.fileToUpload = file.item(0);

    let reader = new FileReader();
    reader.onload = (event: any) => {
      this.imageUrl = event.target.result;
    }
    reader.readAsDataURL(this.fileToUpload);
  }


  selected(){

    if(this.type1=='Food')
    {
      this.typees=1;
      console.log(this.typees);
    }
    else{
      this.typees=0;
      console.log(this.typees);

    }
   }


  addIngredients(newIngredient: string) {
    if (newIngredient!="") {
      this.ingredientsList.push(newIngredient);
    }
    console.log(this.ingredientsList);
  }


}
