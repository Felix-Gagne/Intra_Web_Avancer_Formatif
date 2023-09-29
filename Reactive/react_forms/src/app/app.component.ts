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
    description : ["", [Validators.required]]
  }, 
  {
    validators: [this.numberWordValidator, this.numberOfLettersValidator]
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
    console.log(number);

    //Regarder si les champs sont rempli
    if(number == null || description == null)
    {
      return null;
    }

    //Faire la validation
    const words = description.split(/\s+/);
    console.log(words);

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

  numberOfLettersValidator(control : AbstractControl): ValidationErrors | null
  {
    const number = parseInt(control.get('number')?.value);
    const description = control.get('description')?.value;

    if(number == null || description == null)
    {
      return null;
    }

    const words = description.split(/\s+/);
    let formValid = true;

    for(const w of words)
    {
      console.log("word lenght: " + Array.from(w).length);
      console.log("mot: " + w)
      if(Array.from(w).length !== number)
      {
        formValid = false;
        break;
      }
    };

    if(!formValid)
    {
      control.get('description')?.setErrors({numberOfLetters: true});
      return {nbOfLetters : true};
    }
    else{
      return null;
    }
  }
}

interface Data {
  number?: string | null;
  description?: string | null;
}