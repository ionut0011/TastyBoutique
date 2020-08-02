import { FilterModel } from './filter.model';

export type FiltersModel = {
  count: number;
  pageIndex: number;
  pageSize: number;
  results: FilterModel[];
};
