
import { ReactiveFormsModule, FormsModule, FormControl,FormGroup, Validators, FormBuilder ,  ValidatorFn} from '@angular/forms';
import { Component , OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, Router } from '@angular/router';
import { UsuarioService } from '../../services/usuario.service';
import { error, info } from 'console';


@Component({
  selector: 'app-sing-in-form',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule, ReactiveFormsModule],
  templateUrl: './sing-in-form.component.html',
  styleUrl: './sing-in-form.component.css'
})
export class SingInFormComponent implements OnInit  {
  tieneError : string = "";
  Usuarios: FormGroup; 
  

  ngOnInit(): void {
  this.Usuarios = this.formBuilder.group({
    username : new FormControl('', Validators.required),
    password : new FormControl('', Validators.required)
  
    });
    
  }



  constructor(private formBuilder: FormBuilder,private router: Router, private usuarioServicio:UsuarioService) {
    this.Usuarios = this.formBuilder.group({
      username: ['', <ValidatorFn>Validators.required],
      password: ['', Validators.required],
    });
  };

  

  

  hasError(controlName: string): boolean {
    const control = this.Usuarios.get(controlName);

    // Verifica primero si 'control' es undefined o null.
    if (!control) {
      return false;
    }
    return control.invalid && (control.dirty || control.touched);
  };
  

  mostrar() {
    const { username, password } = this.Usuarios.value; 
    this.usuarioServicio.login(username, password).subscribe({
      next: (userData) => {
        console.log(userData);
        localStorage.setItem('token', userData.token);
        localStorage.setItem('username', username);
      },
      error: (errorData) => {
        console.error(errorData);
        alert("Credenciales erroneas")
      },
      complete: () => {
        console.info("Login Completo");
        alert(`Bienvenido ${username}`);
        this.Usuarios.reset();
        this.router.navigate(['Menu']);
      }
    });
}

recuperar() {
  const { username } = this.Usuarios.value; 
  this.usuarioServicio.recuperar(username).subscribe({
    next: (userData) => {
      console.log(userData);
      localStorage.setItem('email' , userData);
    },
    error: (errorData) => {
      console.error(errorData);
      alert("No se encomtro al Usuario")
    },
    complete: () => {
      console.info("busqueda completa");
      alert(`Mail encontrado`);
    }
  });
}

// get passwordField(): FormControl<string>{
//   return this.Usuarios.controls.password;
// }
}
