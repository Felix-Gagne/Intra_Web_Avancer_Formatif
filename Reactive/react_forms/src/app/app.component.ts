import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, ValidationErrors, Validators } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  title = 'react_forms';
  formData?: Data;

  constructor(private fb : FormBuilder) {}

  form = this.fb.group({
    number : ["", [Validators.required, Validators.min(1), Validators.max(9)]],
    description : ["", [Validators.required, this.numberWordValidator]]
  });

  ngOnInit(): void{
    this.form.valueChanges.subscribe(() => {
      this.formData = this.form.value;
    });
  }


  numberWordValidator(control : AbstractControl): ValidationErrors | null
  {
    //Aller chercher la valeur
    const number = parseInt(control.get('number')?.value);
    const description = control.get('description')?.value;

    //Regarder si les champs sont rempli
    if(number == null || isNaN(number) || description == null)
    {
      return null;
    }

    //Faire la validation
    const words = description.split(/\s+/);

    const numberOfWords = words.length;

    if(numberOfWords != number)
    {
      control.get('description')?.setErrors({numberOfWords: true});
      return {numberOfWords: true}
    }
    else
    {
      control.get('description')?.setErrors(null);
      return null;
    }
  }
}

interface Data {
  number?: string | null;
  description?: string | null;
}