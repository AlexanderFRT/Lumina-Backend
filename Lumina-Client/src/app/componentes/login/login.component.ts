import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { HeaderComponent } from '../header/header.component';
import { SingInFormComponent } from '../sing-in-form/sing-in-form.component';
import { SingUpFormComponent } from '../sing-up-form/sing-up-form.component';
import { ReactiveFormsModule, FormsModule, FormControl, Validators, FormBuilder} from '@angular/forms';
import { SonIguales } from '../../validators/customvalidators';
import { Router } from '@angular/router';
import { RegistroService } from '../../services/registro.service';



@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, RouterLink,HeaderComponent, SingInFormComponent,FormsModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent  {

  signUpMode: boolean = false;
  shouldShowForm: boolean = false;
  

  private readonly nuevoUsuario = inject(FormBuilder);


  formGroup = this.nuevoUsuario.nonNullable.group({
  username :['', Validators.required],
  password : ['', [Validators.required, Validators.minLength(8), Validators.pattern('^(?=.*[A-Z])(?=.*\\d).+$'), ]],
  email : ['', [Validators.required, Validators.email]],
  confirmarpass: ['', [Validators.required,]],

},
{
    validators: SonIguales,
}
  );


constructor(private router: Router,  private registroServicio:RegistroService) { }


Registrar() {
  const {username = '', password = '', email = ''} = this.formGroup.value;
  this.registroServicio.registro(username,password,email).subscribe({
    next: (userData) => {
      console.log(userData);
      localStorage.setItem('token', userData.token);
    },
    error: (errorData) => {
      console.error(errorData);
      alert("Usuario Ya Registrado")
    },
    complete: () => {
      console.info("Login Completo");
      alert(`Haz creado tu cuenta ${username}`);
      this.formGroup.reset();
      this.toggleSignUpMode();
    }

  });
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
      return this.formGroup.controls.username;
    }

    get passField(): FormControl<string>{
      return this.formGroup.controls.password;
    }

  

  toggleSignUpMode() {
    this.signUpMode = !this.signUpMode;
    
    setTimeout(() => {
      this.shouldShowForm = !this.shouldShowForm;
    }, 1000);
    
  }

}
