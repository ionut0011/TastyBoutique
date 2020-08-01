import { RecipesModel } from './recipes.model';

export type RecipessModel = {
  count: number;
  pageIndex: number;
  pageSize: number;
  results: RecipesModel[];
};
