import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Usuario } from '../../Componentes/Models/Usuario';
import { DatePipe } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  constructor(private http:HttpClient) { }

  /* el token es cuando se logea y se almacena en cookie 
  getUsuario(token:string):Observable<Usuario>{
    //falta el url
    //return this.http.get<Usuario>('http:asdasd') 
  }
  */
    setUsuario(usuario:Usuario):Observable<any>{
      return this.http.post<any>('',{usuario});
    } 


  getUsuario(){
    return this.usuario; 
  }
  usuario:Usuario={
    username:"pedrito casas",
    fullName:"",
    dni:"95704858",
    adress:"string",
    email:"email@gmail.com",
    dateOfBirn:new DatePipe("15"),
    password:"string",
    profileImage:"https://media.licdn.com/dms/image/C4D0BAQHJ8T4EUDhVTQ/company-logo_200_200/0/1657893488557/nocountryforjuniordevs_logo?e=1721260800&v=beta&t=WvOWWvqtLs7I1cMCCx2GqAEhGa-EFLmkqc7Q-hQdUpY",
    sessionToken:"",
    verificado:false
    }
}
