import { Component, } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';


@Component({
  selector: 'app-conf-recovery',
  standalone: true,
  imports: [FormsModule,  CommonModule, ConfRecoveryComponent, RouterLink],
  templateUrl: './conf-recovery.component.html',
  styleUrl: './conf-recovery.component.css'
})
export class ConfRecoveryComponent {
    contrasena: string = '';
    newcontrasena: string = '';
    coinciden: boolean = false;

    comparar(){
      if (this.contrasena == this.newcontrasena) {
        this.coinciden = true;
      } else {
        alert("verifique sus contraseñas");
      }
      if (this.coinciden){
        alert("Contraseña Cambiada correctamente")
      }
    }
}
