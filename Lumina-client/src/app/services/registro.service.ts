import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RegistroService {

  constructor(private http: HttpClient) { }

  

  registro(username: string, password: string, email: string): Observable<any> {
    return this.http.post<any>('https://luminabank.somee.com/api/Registration', { username, password, email });
  }
}
