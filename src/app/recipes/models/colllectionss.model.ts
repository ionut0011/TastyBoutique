import { CollectionsModel } from './collections.model';

export type CollectionssModel = {
  count: number;
  pageIndex: number;
  pageSize: number;
  results: CollectionsModel[];
};
