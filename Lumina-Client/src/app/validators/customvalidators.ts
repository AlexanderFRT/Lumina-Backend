import {  AbstractControl, ValidationErrors, Validators, ValidatorFn } from '@angular/forms';


//al comparar elementos de un mismo formulario debo usar formgroup
export const SonIguales : ValidatorFn = (
  formGroupControl: AbstractControl<{password: string; confirmarpass: string}>
): ValidationErrors | null => {
  const pass = formGroupControl.value.password;
  const confirmarpass = formGroupControl.value.confirmarpass;


  return pass !== confirmarpass ? {SonIguales: true} : null;

  };
