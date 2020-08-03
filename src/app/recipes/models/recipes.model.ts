import { FilterModel } from './filter.model';
import { IngredientModel } from './ingredient.model';

export type RecipesModel = {
  id?: string;
  name: string;
  access: boolean;
  description: string;
  type:number;
  filtersList: string[];
  ingredientsList:string[];

};
