import { AsyncPipe, TitleCasePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { UsuarioService } from '../../services/usuario.service';
import { Observable } from 'rxjs';
import { Usuario } from '../../Models/Usuario';
import { RouterLink, RouterOutlet,  } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';


@Component({
  selector: 'app-perfil',
  standalone: true,
  imports: [AsyncPipe,TitleCasePipe,RouterLink,RouterOutlet],
  templateUrl: './perfil.component.html',
  styleUrl: './perfil.component.css'
})
export class PerfilComponent implements OnInit {
usuario: Usuario | undefined;
  
  constructor(private usuarioServicio:UsuarioService, private cookies: CookieService ){}

  ngOnInit(): void {
    this.enviar();
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
      next: (userData: any) => {
        // Suponiendo que userData es el objeto de respuesta que contiene la estructura mostrada
        console.log(userData);
        // Acceder al objeto de usuario dentro de la propiedad "user"
        const userObject = userData.user;
        
        if (userObject) {
            // Asignar el objeto de usuario a this.usuario
            this.usuario = { ...userObject };
            
            // Aquí puedes acceder a los datos específicos del usuario
            console.log('Datos del usuario:', this.usuario);
        } else {
            console.error('No se encontraron datos de usuario en la respuesta');
        }
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
