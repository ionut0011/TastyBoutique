import { FilterModel } from './filter.model';

export type RecipesModel = {
  id?: string;
  name: string;
  access: boolean;
  description: string;
  type:number;
  filtersList: string[];
  ingredientsList:string[];

};
