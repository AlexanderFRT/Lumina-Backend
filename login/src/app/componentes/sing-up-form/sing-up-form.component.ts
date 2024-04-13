import { ReactiveFormsModule, FormsModule, FormControl, Validators, FormBuilder} from '@angular/forms';
import { Component, inject} from '@angular/core';
import { SonIguales } from '../../validators/customvalidators';
import { Router } from '@angular/router';




@Component({
  selector: 'app-sing-up-form',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './sing-up-form.component.html',
  styleUrl: './sing-up-form.component.css'
})
export  class SingUpFormComponent  {

  private readonly nuevoUsuario = inject(FormBuilder);


  formGroup = this.nuevoUsuario.nonNullable.group({
  name :['', Validators.required],
  pass : ['', [Validators.required, Validators.minLength(8), Validators.pattern('^(?=.*[A-Z])(?=.*\\d).+$'), ]],
  email : ['', [Validators.required, Validators.email]],
  confirmarpass: ['', [Validators.required,]],

},
{
    validators: SonIguales,
}
  );


constructor(private router: Router) { }

Registrar() {
  const formData = this.formGroup.value;
  console.log(formData);
  
  alert('Usuario Creado Correctamente');
  localStorage.setItem('userData', JSON.stringify(formData));
  window.location.reload();
}


  
    hasError(controlName: string): boolean {
      const control = this.formGroup.get(controlName);
  
      // Verifica primero si 'control' es undefined o null.
      if (!control) {
        return false;
      }
      return control.invalid && (control.dirty || control.touched);
    };


    get emailField(): FormControl<string>{
      return this.formGroup.controls.email;
    }

    get confirmarpassField(): FormControl<string>{
      return this.formGroup.controls.confirmarpass;
    }

    get nameField(): FormControl<string>{
      return this.formGroup.controls.name;
    }

    get passField(): FormControl<string>{
      return this.formGroup.controls.pass;
    }



}
