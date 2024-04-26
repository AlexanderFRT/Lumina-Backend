import { AsyncPipe, TitleCasePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { UsuarioService } from '../../../core/services/usuario.service';
import { Observable } from 'rxjs';
import { Usuario } from '../../Models/Usuario';
import { RouterLink, RouterOutlet,  } from '@angular/router';


@Component({
  selector: 'app-perfil',
  standalone: true,
  imports: [AsyncPipe,TitleCasePipe
    ,RouterLink,RouterOutlet],
  templateUrl: './perfil.component.html',
  styleUrl: './perfil.component.css'
})
export class PerfilComponent implements OnInit {
  
  public usuario$!:Usuario
  constructor(private service:UsuarioService){}

  ngOnInit(): void {
    this.usuario$=this.service.getUsuario();
  }
   show_editComponent:boolean=false;
  toggleComponent(){
   
    this.show_editComponent=!this.show_editComponent;
  }
}
