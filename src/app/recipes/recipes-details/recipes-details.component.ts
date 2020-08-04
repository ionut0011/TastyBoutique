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

  get test(): FormControl {
    return this.formGroup.get('test') as FormControl;
  }

  get ingredientsList(): FormControl {
    return this.formGroup.get('ingredientsList') as FormControl;
  }

  get test2(): FormControl {
    return this.formGroup.get('test2') as FormControl;
  }

  get filtersList(): FormControl {
    return this.formGroup.get('filtersList') as FormControl;
  }

  get type(): FormControl {
    return this.formGroup.get('type') as FormControl;
  }

  get test3(): FormControl {
    return this.formGroup.get('test3') as FormControl;
  }




  filterssList:FiltersModel;

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
      this.filterssList = data;

      console.log(this.filterssList);
      console.log(data);

    });

    this.formGroup = this.formBuilder.group({
      id: new FormControl(),
      name: new FormControl(),
      description: new FormControl(),
      access: new FormControl(false),
      test: new FormControl(),
      ingredientsList: new FormControl([]),
      test2: new FormControl(),
      filtersList: new FormControl(),
      type:new FormControl(),
      test3:new FormControl(),

   });




    if (this.router.url === '/create-recipe') {
      this.isAddMode = true;
    } else {

      //Getting id from url
      this.routeSub = this.activatedRoute.params.subscribe(params => {
        //Getting details for the trip with the id found

        this.service.get(params['id']).subscribe((data: RecipesGetModel) => {

          this.formGroup.patchValue(data);
          console.log(data);

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


   let finalRecipeModel:RecipesModel=this.formGroup.getRawValue();

    if(this.imageUrl!=undefined)
    {
      finalRecipeModel.image = this.imageUrl.split(',')[1];
    }
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


  additems():void
  {
    this.ingredientsList.value.push(this.test.value);
    this.test.setValue('');

  }


  filterSelected(){
    this.filtersList.setValue(this.test2.value);
    console.log(this.filtersList);

   }

  selected(){
    this.type.setValue(this.test3.value);
    console.log(this.type.value);
   }




}
