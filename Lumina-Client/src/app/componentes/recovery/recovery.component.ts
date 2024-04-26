import { Component, } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import {ConfRecoveryComponent }from '../conf-recovery/conf-recovery.component'
import { RouterLink } from '@angular/router';
import { HeaderComponent } from '../header/header.component';



@Component({
  selector: 'app-recovery',
  standalone: true,
  imports: [FormsModule,  CommonModule, ConfRecoveryComponent, RouterLink, HeaderComponent],
  templateUrl: './recovery.component.html',
  styleUrl: './recovery.component.css'
})
export class RecoveryComponent {
  email: string = '';
  codigo: string = '';
  cambioContrasena: boolean = false;
  codigoSecreto: string = '123456';
  contador = 0;

  MostrarCambio(){
    console.log(this.codigo);
    if (this.contador < 3) {
      if (this.codigo == this.codigoSecreto) {
          this.cambioContrasena = true;
      } else {
          this.contador += 1;
          alert(`Código incorrecto. Intenta de nuevo. Intento fallido: ${this.contador}`);
          
      }
  } else {
      alert('Usuario bloqueado. Has excedido el número de intentos.');
  }

    }

    ngOnInit() {
      // Access localStorage here:
      const email = localStorage.getItem('email');
      if (email) {
        this.email = email;
      } else {
        return
      }
    
  }
}


