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
  });

  ngOnInit(): void{
    this.form.valueChanges.subscribe(() => {
      this.formData = this.form.value;
    });
  }


  numberUnderNine(control : AbstractControl): ValidationErrors | null
  {
    //Aller chercher la valeur
    const number = parseInt(control.get('number')?.value);

    //Regarder si le champ est rempli
    if(number == null)
    {
      return null;
    }

    //Faire la validation
    let formValid = false;

    if(number <= 9 && number >= 1)
    {
      formValid = true;
    }
    else{
      formValid = false;
    }

    //Mettre le champ en erreur
    if(!formValid)
    {
      control.get('number')?.setErrors({numberUnderNine: true});
    }
    else
    {
      control.get('number')?.setErrors({numberUnderNine: false});
    }

    return !formValid?{numberUnderNine:true}:null;
  }
}

interface Data {
  number?: string | null;
}