import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Recipes } from '../models/recipes';

@Component({
  selector: 'app-recipe-list',
  templateUrl: './recipes-list.component.html',
  styleUrls: ['./recipes-list.component.css'],
})
export class RecipesListComponent implements OnInit {
  public recipesList: Recipes[];

  constructor(private router: Router) {}

  public ngOnInit(): void {
    this.recipesList = [
      {
        id: '1',
        name: 'Paste fksdhfkjdshfkjhsdkjfhsdljf',
        description:
          'Delicios',
        backgroundImage: '../../assets/images/paste.jpg',
        isPrivate: false,
      },

      {
        id: '2',
        name: 'Clatite',
        description: 'Yummy',
        backgroundImage: '../../assets/images/clatite.jpg',
        isPrivate: false,
      },

      {
        id: '3',
        name: 'Smoothie cu banana',
        description:
          'Cea mai buna bautura',
        backgroundImage: '../../assets/images/bananasmoothie.jpg',
        isPrivate: false,
      },
      {
        id: '4',
        name: 'Supa de cartofi',
        description:
          '',
        backgroundImage: '../../assets/images/soup.jpg',
        isPrivate: false,
      },

      {
        id: '5',
        name: 'Pizza',
        description: 'Yoummy',
        backgroundImage: '../../assets/images/pizza.jpg',
        isPrivate: false,
      },


    ];
  }

  goToRecipe(id: string): void {
    console.log(id);
    this.router.navigate(['/recipes/details']);
  }
}
