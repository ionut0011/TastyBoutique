import { RecipesGetModel } from './recipesget.model';

export type RecipessModel = {
  count: number;
  pageIndex: number;
  pageSize: number;
  results: RecipesGetModel[];
};
