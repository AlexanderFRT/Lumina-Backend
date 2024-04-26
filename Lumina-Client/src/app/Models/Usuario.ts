import { DatePipe } from "@angular/common";

export interface Usuario {
    address:string;
    dateOfBirth:DatePipe;
    dni:string;
    email:string;
    fullName:string;
    password:string;
    profileImage:string;
    sessionToken:string;
    userName:string;

    // agregar em back
    verificado:boolean
}