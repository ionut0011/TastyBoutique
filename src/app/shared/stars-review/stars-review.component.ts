import { Component, OnInit, Input} from '@angular/core';
import {RecipesGetModel} from '../../recipes/models'
@Component({
  selector: 'app-stars-review',
  templateUrl: './stars-review.component.html',
  styleUrls: ['./stars-review.component.css']
})
export class StarsReviewComponent implements OnInit {
@Input() recipeList: RecipesGetModel[];
  constructor() { }

  ngOnInit(): void {
    console.log(this.recipeList);
  }

}
