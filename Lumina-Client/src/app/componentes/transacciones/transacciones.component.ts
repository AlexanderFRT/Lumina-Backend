import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, FormsModule, FormControl, Validators } from '@angular/forms';
import { TransaccionesService } from '../../services/transacciones.service';
import { CookieService } from 'ngx-cookie-service';
import { cuentarequest } from '../../interfaces/Cuenta';
import { CommonModule } from '@angular/common';
 
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

  depositar(){};

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
}
