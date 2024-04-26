import { DatePipe } from "@angular/common";

export interface Usuario {
    adress:string;
    dateOfBirn:DatePipe;
    dni:string;
    email:string;
    fullName:string;
    password:string;
    profileImage:string;
    sessionToken:string;
    username:string;

    // agregar em back
    verificado:boolean
}