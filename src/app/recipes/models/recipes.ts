export class Recipes {
  public id: string;
  public name: string;
  public access:string;
  public description: string;
  public backgroundImage?: string;
  public type:number;
  public ingredients:string[] =[];
  public filtersList: string[] = [];

}
