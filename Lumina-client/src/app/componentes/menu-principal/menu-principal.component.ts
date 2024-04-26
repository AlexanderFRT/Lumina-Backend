
import { ReactiveFormsModule, FormsModule, FormControl,FormGroup, Validators, FormBuilder ,  ValidatorFn} from '@angular/forms';
import { Component , OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, Router } from '@angular/router';
import { UsuarioService } from '../../services/usuario.service';
import { CookieService } from 'ngx-cookie-service';
import { FooterComponent } from '../footer/footer.component';



@Component({
  selector: 'app-menu-principal',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule, ReactiveFormsModule, FooterComponent ],
  templateUrl: './menu-principal.component.html',
  styleUrl: './menu-principal.component.css'
})
export class MenuPrincipalComponent implements OnInit {
  username: string = '';
  Perfil : FormGroup;
  
  ngOnInit(): void {
    this.Perfil = this.formBuilder.group({
      fullname : new FormControl('', Validators.required),
      DateOfBirth : new FormControl('', Validators.required),
      Address : new FormControl('', Validators.required),
      DNI : new FormControl('', Validators.required)
      
    
      });
      const username = this.cookies.get('username');
      if (username) {
        this.username = username;
      } else {
        return
      }
    }

  constructor (private router: Router, private usuarioServicio : UsuarioService, private formBuilder: FormBuilder ,private cookies : CookieService){
    this.Perfil = this.formBuilder.group({
      fullname : new FormControl('', Validators.required),
      DateOfBirth : new FormControl('', Validators.required),
      Address : new FormControl('', Validators.required),
      DNI : new FormControl('', Validators.required)
  })}

  completar(){
    console.log(this.Perfil);
    const token = this.cookies.get("token");
    const { fullname, DNI, DateOfBirth, Address } = this.Perfil.value;
  
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
      }
    });
  }

  

  salir() {
    this.cookies.delete("username");
    this.cookies.delete("token");
    localStorage.clear();
    this.router.navigate(['']);
  }

  enviar() {
    // Obtener el token almacenado en el almacenamiento local
    const token = this.cookies.get("token");
  
    // Verificar si el token está presente
    if (!token) {
      console.error('No se encontró el token en el almacenamiento local');
      return; // Si no hay token, detener la ejecución de la función
    }
  
    // Realizar la solicitud al servicio utilizando el token de autorización
    this.usuarioServicio.recibir(token).subscribe({
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
      }
    });
  }
}
