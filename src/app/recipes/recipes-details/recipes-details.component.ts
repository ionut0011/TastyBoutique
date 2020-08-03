import { Component, OnInit,OnDestroy} from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';

import { RecipesModel, RecipesGetModel,FilterModel,FiltersModel, IngredientModel } from '../models';
import { RecipeService } from '../services/recipe.service';
import { isNgContainer } from '@angular/compiler';


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


  filtersList:FiltersModel;
  filterSend:string[]=[];
  ingredientsList:string[] =[];
  type:number;


  type1:FormControl=new FormControl();
  filter:FormControl=new FormControl();
  typeesList: string[] = ['Food', 'Drink'];
  foodordrink:string[] =[];



  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private service: RecipeService
    ) { }

  ngOnInit(): void {

    this.service.getAllFilters().subscribe((data: FiltersModel) => {
      this.filtersList = data;

      console.log(this.filtersList);
      console.log(data);

    });

    this.formGroup = this.formBuilder.group({
      id: new FormControl(),
      name: new FormControl(),
      description: new FormControl(),
      access: new FormControl(false)
    })



    if (this.router.url === '/create-recipe') {
      this.isAddMode = true;
    } else {

      //Getting id from url
      this.routeSub = this.activatedRoute.params.subscribe(params => {
        //Getting details for the trip with the id found

        this.service.get(params['id']).subscribe((data: RecipesGetModel) => {


          this.filterSend=[];
          this.ingredientsList=[];


          data.ingredientsList.forEach(element => {this.ingredientsList.push(JSON.parse(JSON.stringify(element)).name);
        });
        data.filtersList.forEach(element => {this.filterSend.push(JSON.parse(JSON.stringify(element)).name);
        });

          // data.filtersList= this.checkFilters(this.filterSend);
       //   data.ingredientsList=this.checkIngredients(this.ingredientsList);
          console.log(data);
          data.type=this.type;

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

      var finalRecipeModel:RecipesModel=this.formGroup.getRawValue();

      finalRecipeModel.filtersList= this.filterSend;
      finalRecipeModel.ingredientsList=this.ingredientsList;
      finalRecipeModel.type=this.type;
      finalRecipeModel.image = this.imageUrl;
    if (this.isAddMode) {



     this.service.post(finalRecipeModel).subscribe();
     this.router.navigate(['list']);
    } else {

      this.service.patch(finalRecipeModel).subscribe();
      this.router.navigate(['list']);
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

    if(this.type1.value=='Food')
    {
      this.type=1;
      console.log(this.type);
    }
    else{
      this.type=0;
      console.log(this.type);

    }
   }

   filterSelected(){


    this.filterSend.push(this.filter.value);
    console.log(this.filterSend);

   }

  addIngredients(newIngredient: string) {
    if (newIngredient!="") {
      this.ingredientsList.push(newIngredient);
    }
    console.log(this.ingredientsList);
  }
}
