import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ReactiveFormsModule} from '@angular/forms'
@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css'],
})
export class CreateComponent implements OnInit {
  public form: FormGroup;
  constructor() {}

  ngOnInit(): void {
    this.form = new FormGroup({
      recipeName: new FormControl('', [Validators.required]),
      recipeDetails: new FormControl('', [])
    });
  }

  public get isFormValid(): boolean {
    return this.form.valid;
  }

  public clicked(): void {
    console.log(this.form.value);
  }
}

