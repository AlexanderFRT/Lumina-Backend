import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, Router } from '@angular/router';
import { cuentarequest } from '../../interfaces/Cuenta';
import { TransaccionesService } from '../../services/transacciones.service';
import { CookieService } from 'ngx-cookie-service';


@Component({
  selector: 'app-cuentas',
  standalone: true,
  templateUrl: './cuentas.component.html',
  styleUrl: './cuentas.component.css',
  imports: [CommonModule, RouterLink]
})
export class CuentasComponent implements OnInit {
  listaCuentas: cuentarequest[] = [];

  ngOnInit(): void {
      this.datos();

      this.transaccionesServicio.listarCuentas(this.cookies.get('token')).subscribe(cuentas => {
        this.listaCuentas = cuentas;
      });

  }
constructor(private transaccionesServicio : TransaccionesService, private cookies : CookieService){
};

  datos() {
    const  token  = this.cookies.get('token');
    this.transaccionesServicio.listarCuentas(token).subscribe({
      next: (cuentaData) => {
        console.log(cuentaData);
      },
      error: (errorData) => {
        console.log(errorData);
          
      },
      complete() {
          console.log("completo");
      },
    })
  }

  getTipoCuenta(type: string): string {
    if (type == '1') {
      return 'Cuenta de Ahorro';
    } else if (type == '0') {
      return 'Cuenta Corriente';
    } else {
      return 'Tipo de cuenta desconocido';
    }
  }

  calcularSaldoTotal(cuentas: cuentarequest[]): number {
    let saldoTotal = 0;
    for (let cuenta of cuentas) {
      saldoTotal += parseInt(cuenta.balance);
    }
    return saldoTotal;
  }
}
