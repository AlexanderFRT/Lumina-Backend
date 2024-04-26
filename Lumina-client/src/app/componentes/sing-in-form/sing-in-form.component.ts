
import { ReactiveFormsModule, FormsModule, FormControl,FormGroup, Validators, FormBuilder ,  ValidatorFn} from '@angular/forms';
import { Component , OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, Router } from '@angular/router';
import { UsuarioService } from '../../services/usuario.service';
import { CookieService } from 'ngx-cookie-service'


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
  visible : boolean = false;
  inputType: string = 'password';

  ngOnInit(): void {
  this.Usuarios = this.formBuilder.group({
    username : new FormControl('', Validators.required),
    password : new FormControl('', Validators.required)
  
   
    });
   
  }



  constructor(private formBuilder: FormBuilder,private router: Router, private usuarioServicio:UsuarioService,  private cookies : CookieService) {
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
        this.cookies.set("username",username);
        this.cookies.set('token', userData.token);
        this.cookies.set('Status', userData.status);
        console.log(userData)
      },
      error: (errorData) => {
        console.error(errorData);
        alert("Credenciales erroneas")
      },
      complete: () => {
        console
        console.info("Login Completo");
        alert(`Bienvenido ${username}`);
        this.Usuarios.reset();
       if (this.cookies.get('Status') == 'Unverified') {
        this.router.navigate(['VerificarCuenta']);
       } else {
        this.router.navigate(['cuenta'])
       }
      }
    });
}

//metodo para recuperar con nombre de usuario 
// recuperar() {
//   const { username } = this.Usuarios.value; 
//   this.usuarioServicio.recuperar(username).subscribe({
//     next: (userData) => {
//       console.log(userData);
//       this.cookies.set('email' , userData);
//     },
//     error: (errorData) => {
//       console.error(errorData);
//       alert("No se encomtro al Usuario")
//     },
//     complete: () => {
//       console.info("busqueda completa");
//       alert(`Mail encontrado`);
//     }
//   });
// }

// get passwordField(): FormControl<string>{
//   return this.Usuarios.controls.password;
// }


//Funcion para cambiar icono y mostrar contraseña
mostrarContra(){
  this.visible = !this.visible;
  this.inputType = (this.inputType === 'password') ? 'text' : 'password';
}


}
