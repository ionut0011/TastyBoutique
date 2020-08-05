import { Component, OnInit,OnDestroy} from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { RecipesModel, RecipesGetModel,FilterModel,FiltersModel, IngredientModel } from '../models';
import { RecipeService } from '../services/recipe.service';
import { isNgContainer } from '@angular/compiler';
import {CommentModel} from '../models/comment.model';
import { Params } from '@angular/router';
import {LoginComponent} from '../../login/login/login.component'

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
  ratingNumber: number = 0;

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

  get filtersList(): FormControl {
    return this.formGroup.get('filterSend') as FormControl;
  }

  get type(): FormControl {
    return this.formGroup.get('type') as FormControl;
  }

  filterssList:FiltersModel;
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
      this.filterssList = data;
      console.log(this.filtersList);
      console.log("filtersList", data);
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
      filtersList: new FormControl([]),
      type:new FormControl()

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
          console.log(data);
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
      let finalRecipeModel:RecipesModel=this.formGroup.getRawValue();
      finalRecipeModel.image = this.imageUrl.split(',')[1];
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

  testStar(rating) {
    this.ratingNumber = rating;
    console.log(this.ratingNumber);
  }

  refresh(): void {
    window.location.reload();
}
  postComment(){
    const commentsModel : CommentModel = this.formGroupComment.getRawValue();
    const comment = commentsModel.comment;
    commentsModel.review = this.ratingNumber;
    console.log(commentsModel.review);
    this.routeSub = this.activatedRoute.params.subscribe(params => {
      this.service.addComment(params['id'], commentsModel).subscribe((data: CommentModel) => {
        console.log('post comment:', data);
        this.refresh();
      });
        console.log("s-a adaugat commentul");
        console.log(commentsModel);
    });
}

public deleteComment(recipeId: string, commentId :string) :void{
  console.log("ID COMMENT:", commentId, "Id recipe:", recipeId);
  this.service.deleteComment(recipeId, commentId).subscribe(data => {
    console.log(data);
    this.refresh();
  })
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
    this.filtersList.value.push(this.test2.value);
    console.log(this.filtersList);
   }


  selected(){
    if(this.type.value=='Food')
    {
      this.type.setValue(1);
    }
    else{
      this.type.setValue(2);
    }
   }

}
