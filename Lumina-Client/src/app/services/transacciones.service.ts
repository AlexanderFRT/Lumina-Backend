import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TransaccionesService {

  constructor(private http: HttpClient) {   }


  listarCuentas(token: string): Observable<any> {
    // Crea el encabezado de la peticion
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });  
    // Enviar la solicitud POST al backend
    return this.http.get<any>('https://luminabank.somee.com/api/Deposits/ListaDeCuentas',  { headers: headers });
  }



  depositar (accountNumber : string, amount : string,cardNumber: string, expiryDate: string, cvv: string ): Observable<any> {
    
    const accountNumberInt = parseInt(accountNumber, 10);
    const amountInt = parseFloat(amount);
    // Enviar la solicitud POST al backend
    return this.http.post<any>('https://luminabank.somee.com/api/Deposits/DepositarSaldo',  { accountNumberInt, amountInt , cardNumber, expiryDate, cvv});
  }
}
