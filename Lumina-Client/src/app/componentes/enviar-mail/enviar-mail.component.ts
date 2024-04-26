import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ConfRecoveryComponent } from '../conf-recovery/conf-recovery.component';
import { Router, RouterLink } from '@angular/router';
import { UsuarioService } from '../../services/usuario.service';

@Component({
  selector: 'app-enviar-mail',
  standalone: true,
  imports: [FormsModule,  CommonModule, ConfRecoveryComponent, RouterLink,ReactiveFormsModule],
  templateUrl: './enviar-mail.component.html',
  styleUrl: './enviar-mail.component.css'
})
export class EnviarMailComponent  {

formGroup : FormGroup;



  constructor(private formBuilder: FormBuilder, private usuarioServicio: UsuarioService, private router: Router) {
    this.formGroup = this.formBuilder.group({
      username: ['', Validators.required],
    });
  }

  // recuperar() {
  //   const { username } = this.formGroup.value; 
  //   this.usuarioServicio.recuperar(username).subscribe({
  //     next: (userData) => {
  //       console.log(userData);
  //       localStorage.setItem('email' , userData);
  //     },
  //     error: (errorData) => {
  //       console.error(errorData);
  //       alert("No se encomtro al Usuario")
  //     },
  //     complete: () => {
  //       console.info("busqueda completa");
  //       this.router.navigate(['enviado']);
  //     }
  //   });
  // }

}
