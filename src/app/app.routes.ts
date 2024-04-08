import { Routes } from '@angular/router';
import { EstamosTrabajandoComponent } from './Componentes/estamos-trabajando/estamos-trabajando.component';

export const routes: Routes = [
    {
        path:'EstamosTrabajando',
        component:EstamosTrabajandoComponent
    },
    {
        path:'',
        redirectTo:"",
        pathMatch:'full'
    },
    
];
