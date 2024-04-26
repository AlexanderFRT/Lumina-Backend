import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormsModule, FormControl,FormGroup, Validators, FormBuilder ,  ValidatorFn} from '@angular/forms';
import { UsuarioService } from '../../services/usuario.service';
import { CookieService } from 'ngx-cookie-service';
import { RouterLink, Router } from '@angular/router';


@Component({
  selector: 'app-verificarcuenta',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule,  RouterLink],
  templateUrl: './verificarcuenta.component.html',
  styleUrl: './verificarcuenta.component.css'
})
export class VerificarcuentaComponent implements OnInit {
validacion :  FormGroup;



  ngOnInit(): void {
    this.validacion = this.formBuilder.group({
      fullname : new FormControl('', Validators.required),
      DateOfBirth : new FormControl('', Validators.required),
      Address : new FormControl('', Validators.required),
      DNI : new FormControl('', Validators.required),
    });
   
  }


    constructor(private formBuilder: FormBuilder, private usuarioServicio : UsuarioService, private cookies : CookieService,private router: Router) {
      this.validacion = this.formBuilder.group({
        fullname : new FormControl('', Validators.required),
        DateOfBirth : new FormControl('', Validators.required),
        Address : new FormControl('', Validators.required),
        DNI : new FormControl('', Validators.required),
      });
    };

    completar(){
      console.log(this.validacion);
      const token = this.cookies.get("token");
      const { fullname, DNI, DateOfBirth, Address } = this.validacion.value;
    
      // Verificar si el token está presente
      if (!token) {
        console.error('No se encontró el token en el almacenamiento local');
        return; // Si no hay token, detener la ejecución de la función
      }
    
      // Realizar la solicitud al servicio utilizando el token de autorización
      this.usuarioServicio.completar(token, fullname, DNI, DateOfBirth, Address).subscribe({
        next: (userData) => {
          // Manejar la respuesta exitosa del servicio
          console.log(userData);
        },
        error: (errorData) => {
          // Manejar cualquier error que se produzca durante la solicitud
          console.error(errorData);
          alert("Credenciales erróneas o solicitud incorrecta");
        },
        complete: () => {
          // Esta parte del código se ejecutará cuando la suscripción esté completa
          console.log('Solicitud completada');
          this.router.navigate(['perfil']);
        }
      });
    }
  
}


