import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  constructor(private http: HttpClient) { }

  login(username: string, password: string): Observable<any> {
    return this.http.post<any>('https://luminabank.somee.com/api/Login', { username, password });
  }

  // recuperar(username: string): Observable<any>{
  //   return this.http.post<any>('http://127.0.0.1:8000/recuperar/', { username });
  // }

  recibir(token: string): Observable<any> {

    // Crea el encabezado de la peticion
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    
    // Enviar la solicitud POST al backend
    return this.http.get<any>('https://luminabank.somee.com/api/UserVerification/datosusuario',  { headers: headers });
  }

  
  completar(token: string, fullname: string, DNI: string, DateOfBirth: string, Address: string): Observable<any> {
    // Convertir DateOfBirth de cadena a objeto Date
    const dateOfBirth = new Date(DateOfBirth);

    // Crear el encabezado de autorización con el token recibido
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    console.log(dateOfBirth);
    // Enviar la solicitud POST al backend
    return this.http.put<any>('https://luminabank.somee.com/api/UserVerification/VerificaciónUsuario',{ fullname, DNI, DateOfBirth, Address }, { headers: headers });
  }
}