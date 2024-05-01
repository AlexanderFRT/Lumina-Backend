import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, FormsModule, FormControl, Validators, NgForm } from '@angular/forms';
import { TransaccionesService } from '../../services/transacciones.service';
import { CookieService } from 'ngx-cookie-service';
import { cuentarequest } from '../../interfaces/Cuenta';
import { CommonModule } from '@angular/common';
import { constants } from 'buffer';
 
@Component({
  selector: 'app-transacciones',
  standalone: true,
  imports: [ FormsModule, ReactiveFormsModule, CommonModule ],
  templateUrl: './transacciones.component.html',
  styleUrl: './transacciones.component.css'
})
export class TransaccionesComponent implements OnInit {
  Deposito : FormGroup;
  listaCuentas: cuentarequest[] = [];


ngOnInit(): void {
  this.datos();
  this.Deposito = this.formBuilder.group({
   

    accountNumber: [''], 
    cardNumber: [''],
    CVV: [''],
    expiryDate: [''],
    amount: ['']
  });

  this.transaccionesServicio.listarCuentas(this.cookies.get('token')).subscribe(cuentas => {
    this.listaCuentas = cuentas;
  });
}

constructor(private formBuilder: FormBuilder, private transaccionesServicio:TransaccionesService, private cookies : CookieService) {
  this.Deposito = this.formBuilder.group({
   

    accountNumber: [''], 
    cardNumber: [''],
    CVV: [''],
    expiryDate: [''],
    amount: ['']
  });
  };


  isActive: boolean = false;

  togglePanel(){
    this.isActive=!this.isActive;
  };

  depositar(){
    const { accountNumber, cardNumber, CVV, expiryDate, amount} = this.Deposito.value;

    if (this.Deposito.valid){
      this.transaccionesServicio.depositar(accountNumber, cardNumber, CVV, expiryDate, amount).subscribe({
        next: (data) => {
          console.log(data);
        },
        error: (error) => {
          console.log(error);
        },
        complete: () => {
          alert("Deposito Completo");
          this.Deposito.reset();
         
        }
        
      });
    }
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

}
