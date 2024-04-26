import { Component, Input, NgModule, OnInit, inject } from '@angular/core';
import { Usuario } from '../../Models/Usuario';
import { RouterLink } from '@angular/router';
import { UsuarioService } from '../../../core/services/usuario.service';
import { rejects } from 'assert';
import { read } from 'fs';
import { error } from 'console';
import { DomSanitizer } from '@angular/platform-browser';
import { FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';


@Component({
  selector: 'app-edit-perfil',
  standalone: true,
  imports: [RouterLink,FormsModule],
  templateUrl: './edit-perfil.component.html',
  styleUrl: './edit-perfil.component.css'
})
export class EditPerfilComponent implements OnInit {  
 
  ngOnInit(): void {
    this.usuario=this.service.getUsuario();
  }

  public usuario!:Usuario
  imagenUrl:string = 'https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_1280.png';
  constructor(private service:UsuarioService, private sanitizar:DomSanitizer){}




  toggleEditMode(seEdito:boolean){
    this.usuario.verificado=seEdito;
  }
  
  onSubmit() {
    console.log(this.usuario.fullName);
    console.log(this.usuario.dni);
    console.log(this.usuario.adress);
    /* if (form.valid) {
      console.log('Datos del formulario:', form.value);
      // Aquí puedes utilizar form.value para obtener los datos del formulario
    } */
  }

  registrar(seEdito:boolean){

    this.service.setUsuario(this.usuario).subscribe({
      next:(userData)=>{
        console.log("que pasa")
      }
    })

    this.toggleEditMode(seEdito);
  }
  
  
 
  
  
  
  
  
  
  
  
  
  selectedFile!: File ;
  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
    this.extraerBase64(this.selectedFile).then((imagen:any) =>{
      this.imagenUrl=imagen.base;
      console.log(  this.imagenUrl)
    })
  }
  
  extraerBase64 = async($event: any) => new Promise ((resolve,reject)=>{
    try{
      const unsafeImg = window.URL.createObjectURL($event);
      const image = this.sanitizar.bypassSecurityTrustUrl(unsafeImg);
      const reader = new FileReader();
      reader.readAsDataURL($event)
      reader.onload = () => {
        resolve({
          base:reader.result
        });
      };
      reader.onerror = error =>{
        resolve({
          base: null
        });
      };
    }catch(e){
      resolve({

        base:null
      })
    }
  })
}
