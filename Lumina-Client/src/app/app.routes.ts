import { Routes } from '@angular/router';
import { EstamosTrabajandoComponent } from './Componentes/estamos-trabajando/estamos-trabajando.component';
import { EditPerfilComponent } from './Componentes/home/edit-perfil/edit-perfil.component';
import { PerfilComponent } from './Componentes/home/perfil/perfil.component';

export const routes: Routes = [
    {
        path:'EstamosTrabajando',
        component:EstamosTrabajandoComponent
    },
    {
        path:'Perfil',
        component:PerfilComponent
    },
    {
        path:'PerfilEdit',
        component:EditPerfilComponent
    },
    {
        path:'',
        redirectTo:"",
        pathMatch:'full'
    },
    
];
