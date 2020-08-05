import { Component, OnInit,OnDestroy} from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription, Observable, Observer } from 'rxjs';
import { RecipesModel, RecipesGetModel,FilterModel,FiltersModel, IngredientModel } from '../models';
import { RecipeService } from '../services/recipe.service';
import {CommentModel} from '../models/comment.model';
import { Ng2ImgMaxService } from 'ng2-img-max';
import { HttpClient, HttpHeaders } from '@angular/common/http';
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

  formGroupComment : FormGroup;

  isAdmin: boolean;
  isAddMode: boolean;
  photos: Blob[] = [];

  public commentsList: CommentModel;
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

  get filter(): FormControl {
    return this.formGroup.get('filter') as FormControl;
  }

  get type(): FormControl {
    return this.formGroup.get('type') as FormControl;
  }

  get test3(): FormControl {
    return this.formGroup.get('test3') as FormControl;
  }




  filterssList:FiltersModel;


  typeesList: string[] = ['Food', 'Drink'];
  foodordrink:string[] =[];
  uploadedImage: FileList;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private service: RecipeService,
    private ng2ImgMax: Ng2ImgMaxService,
    private readonly http :HttpClient
    ) { }


onImageChange(event) {
  //event='../../assets/images/food.jpg';
  let image = event.target.files.item(0);
  console.log(image);

  this.ng2ImgMax.resizeImage(image, 225, 225).subscribe(
    result => {
      this.handleFileInput(result);
    },
    error => {
      console.log('😢 Oh no!', error);
    }
  );
}


  handleFileInput(file: FileList) {
    this.fileToUpload = file;
    let reader = new FileReader();
    reader.onload = (event: any) => {
      this.imageUrl = event.target.result;
      console.log("imgurl=",this.imageUrl);
    }



  }


  ngOnInit(): void {

    this.service.getAllFilters().subscribe((data: FiltersModel) => {
      this.filterssList = data;
    });
    this.routeSub = this.activatedRoute.params.subscribe(params => {
    this.service.getComments(params['id']).subscribe((comments: CommentModel) =>{
      this.commentsList = comments;
      console.log("Comentariile acestei retete", this.commentsList);

    })
  });


    this.formGroup = this.formBuilder.group({
      id: new FormControl(),
      name: new FormControl(),
      description: new FormControl(),
      access: new FormControl(false),
      test: new FormControl(),
      ingredientsList: new FormControl([]),
      test2: new FormControl(),
      filter: new FormControl(),
      type:new FormControl(),
      test3:new FormControl(),

   });


    this.formGroupComment = this.formBuilder.group({
      idRecipe: new FormControl(),

      comment: new FormControl(),
      review: new FormControl()
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

   console.log("urlimage=",this.imageUrl);
    if(this.imageUrl!=undefined)
    {
      finalRecipeModel.image = this.imageUrl.split(',')[1];
    }
    else
    {
      // finalRecipeModel.image = '../../assets/images/food.jpg'
    }
    finalRecipeModel.ingredientsList = this.validateIngredients(finalRecipeModel.ingredientsList);
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


  postComment(){

    const commentsModel : CommentModel = this.formGroupComment.getRawValue();
    const comment = commentsModel.comment;
    commentsModel.review = 3;
    commentsModel.recipeId = "04089499-6e9a-406e-b156-ddf08082686b";

    this.routeSub = this.activatedRoute.params.subscribe(params => {
      this.service.addComment(params['id'], commentsModel).subscribe((data: CommentModel) => {
        console.log('post comment:', data);
      });
        console.log("s-a adaugat comentul");
        console.log(commentsModel);
      });
}


  validateIngredients(ingr :string[])
  {
    let ingredients :string[] = [];
    ingr.forEach(element => {
      if(element['name']!=undefined || element['name']!=null)
      {
          ingredients.push(element['name']);
      }
      else
        ingredients.push(element);

    });
    return ingredients;
  }

  additems():void
  {
    this.ingredientsList.value.push(this.test.value);
    this.test.setValue('');

  }


  filterSelected(){


    this.filter.setValue(this.test2.value);

    console.log(this.filter);
   }

  selected(){

    this.type.setValue(this.test3.value);
    console.log(this.type.value);
   }



}
