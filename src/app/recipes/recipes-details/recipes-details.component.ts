import { Component, OnInit,OnDestroy} from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription, Observable, Observer } from 'rxjs';
import { RecipesModel, RecipesGetModel,FilterModel,FiltersModel, IngredientModel } from '../models';
import { RecipeService } from '../services/recipe.service';
import { CommentsService } from '../services/comments.service';
import {CommentModel} from '../models/comment.model';
import { Ng2ImgMaxService } from 'ng2-img-max';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {ToastrService} from 'ngx-toastr'
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
  validIngredients : boolean = false;
  photos: Blob[] = [];
  ratingNumber: number = 0;
  createdRecipe : boolean = false;
  public commentsList: CommentModel[];
  public recipeList: RecipesModel[]=[];
  private routeSub: Subscription = new Subscription();
  public recipesList: RecipesGetModel[];
  public selectedFilter : boolean = false;
  public selectedType :  boolean = false;
  public buttonDisabled : boolean = false;


  get description(): string {
    return this.formGroup.get('description').value;
  }

  get isFormDisabled(): boolean {
    return this.formGroup.disabled;
  }

  get test(): FormControl {
    return this.formGroup.get('test') as FormControl;
  }

  get ingredients(): FormControl {
    return this.formGroup.get('ingredients') as FormControl;
  }

  public get ingredientsControl() :FormControl{
    return this.formGroup.controls.ingredients as FormControl;
  }

  public get descriptionControl(): FormControl{
    return this.formGroup.controls.description as FormControl;
  }

  public get nameControl(): FormControl{
    return this.formGroup.controls.name as FormControl;
  }

  public get typeControl(): FormControl{
    return this.formGroup.controls.test3 as FormControl;
  }

  public get  filterControl(): FormControl{
    return this.formGroup.controls.test2 as FormControl;
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

  type1:FormControl=new FormControl();


  typeesList: string[] = ['Food', 'Drink'];


  uploadedImage: FileList;


  constructor(
    private toastr: ToastrService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private service: RecipeService,
    private serviceComments: CommentsService,
    private ng2ImgMax: Ng2ImgMaxService,
    private readonly http :HttpClient
    ) {
      this.formGroup = this.formBuilder.group({
        id: new FormControl(),
        name: new FormControl('', [Validators.required]),
        description: new FormControl('', [Validators.required]),
        age : new FormControl('',[]),
        access: new FormControl(false),
        test: new FormControl('', [Validators.required, Validators.pattern("^.*,*.*$")]),
        ingredients: new FormControl([]),
        test2: new FormControl('', [Validators.required]),
        filter: new FormControl(),
        type:new FormControl(),
        test3:new FormControl('', [Validators.required]),

     });

    }


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
    reader.readAsDataURL(this.fileToUpload);
  }


  ngOnInit(): void {
    this.service.getAllFilters().subscribe((data: FiltersModel) => {
      this.filterssList = data;
      console.log(this.filterssList);
      console.log("filtersList", data);
    });
    this.routeSub = this.activatedRoute.params.subscribe(params => {
    this.serviceComments.getComments(params['id']).subscribe((comments: CommentModel[]) =>{
      this.commentsList = comments;
      console.log("Comentariile acestei retete", this.commentsList);
    })
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

          this.test2.setValue(data.filters[0].name);
          this.filter.setValue(data.filters[0].name);
          this.test3.setValue(data.type);

          console.log(data.ingredients);
          this.test.setValue(data.ingredients.map(i =>i.name).join(','));

          console.log("Ingrediente" ,data.ingredients.map(i =>i.name).join(','));

          this.formGroup.patchValue(data);
          console.log("DATA" , data);
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
    finalRecipeModel.ingredients = this.validateIngredients(finalRecipeModel.ingredients);
    if (this.isAddMode)
    {
     this.service.post(finalRecipeModel).subscribe((data : RecipesModel) =>
     {
      console.log(data);
      this.createdRecipe = true;
      if(this.createdRecipe)
      {
        this.toastr.success("Recipe added");
      }
      this.recipeList.push(data);
      console.log("RecipeList:", this.recipeList);
      this.router.navigate(['list']);
     this.getAllRecipes();
     },
     (error) => {
      this.createdRecipe = false;
      this.toastr.error("Please fill all the required fields");
    }
     );

    } else {
      this.service.patch(finalRecipeModel).subscribe(()=>{
        this.toastr.success("Updated")
      },
        (error)=>{
          this.toastr.error("Something went wrong.")
        }
      );
      this.router.navigate(['list']);
    }
    this.photos.push(this.imageUrl);
    this.imageUrl = null;
    this.formGroup.disable();

    console.log(this.recipeList);
  }


  public getAllRecipes(): void{

    this.service.getAll().subscribe((data: RecipesGetModel[]) => {
      this.recipesList = data;
      this.recipesList.forEach(element => {
        if(element.image.length>5){
        let link:any = 'data:image/png;base64,'+element.image;
        element.image = link;
        }
      });

      this.service.saveRecipes(this.recipesList);

    });


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
      this.serviceComments.addComment(params['id'], commentsModel).subscribe((data: CommentModel) => {
        this.commentsList.push(data);
        this.toastr.success('Comment added')
      },
      (error) =>
      {
        this.toastr.error('Something went wrong');
      }
      );
        console.log("s-a adaugat commentul");
        console.log(commentsModel);
    });
}

public deleteComment(recipeId: string, commentId :string) :void{
  console.log("ID COMMENT:", commentId, "Id recipe:", recipeId);
  for (let i=0;i<this.commentsList.length;i++)
  {
      console.log(this.commentsList[i].id);
      if(commentId == this.commentsList[i].id){
        this.serviceComments.deleteComment(recipeId, commentId).subscribe(data => {}
        ,
        (error) =>{
          console.log(error.error.text)
          if(error.error.text == "Deleted")
          {
            this.commentsList.splice(i,1);
            this.toastr.success('Comment deleted');
          }
          else
            this.toastr.error('You can\'t delete this comment.');

        })
    }
  }
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
    var regexp = new RegExp("^.*,*.*$");
     var test = regexp.test(this.test.value);
     console.log(this.test.value);
     console.log("TEST", test);
     if(test)
     {
       this.validIngredients = true;
     }else
     {
       this.validIngredients = false;
       this.toastr.error("Wrong ingredient input")

     }

     if(this.test.value == "")
     {
       this.validIngredients = false;
       this.toastr.error("Wrong ingredient input")
     }

    let splitted= this.test.value.split(",");
    console.log(this.ingredients);
      splitted.forEach(element => {
        this.ingredients.value.push(element);
      });
      console.log(this.ingredients);

  }

  filterSelected(){

    this.filter.setValue(this.test2.value);
    if(this.test2.value)
    {
      this.selectedFilter = true;
    }
    else{
      this.selectedFilter = false;
    }
    console.log(this.filter);
   }

  selected(){

    this.type.setValue(this.test3.value);
    console.log("ingredients test", this.test3.value);
    if(this.test3.value)
    {
      this.selectedType = true;

    }
    else{
      this.selectedType = false;
    }
    console.log(this.selectedType);
    console.log("type", this.type.value);
   }

   public goToPage(page: string): void {
    this.router.navigate([page]);
  }
}
