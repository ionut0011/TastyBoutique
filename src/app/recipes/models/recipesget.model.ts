import { FilterModel } from './filter.model';
import {IngredientModel} from './ingredient.model';

export type RecipesGetModel = {
  id?: string;
  name: string;
  access: boolean;
  description: string;
  type:number;
  averageReview : number;
  filtersList: FilterModel[];
  ingredientsList:IngredientModel[];
  image:BinaryType[];
};