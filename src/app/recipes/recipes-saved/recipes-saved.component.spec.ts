import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RecipesSavedComponent } from './recipes-saved.component';

describe('RecipesSavedComponent', () => {
  let component: RecipesSavedComponent;
  let fixture: ComponentFixture<RecipesSavedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RecipesSavedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RecipesSavedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
