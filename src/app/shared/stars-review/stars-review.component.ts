import { Component, OnInit, Input} from '@angular/core';
import {RecipesGetModel} from '../../recipes/models'
import { RecipeService } from '../../recipes/services/recipe.service';

@Component({
  selector: 'app-stars-review',
  templateUrl: './stars-review.component.html',
  styleUrls: ['./stars-review.component.css']
})
export class StarsReviewComponent implements OnInit {
@Input() recipeList: RecipesGetModel[];

  constructor(private service: RecipeService) { }

  ngOnInit(): void {

    this.service.getAllCollections().subscribe((data: RecipesGetModel[]) => {
      this.recipeList = data;
      this.recipeList.forEach(element => {
        if(element.image.length>5){
        let link:any = 'data:image/png;base64,'+element.image;
        element.image = link;
        }
      });
      console.log(data);
});
  }
}


