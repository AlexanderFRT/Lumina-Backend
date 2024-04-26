import { Routes } from '@angular/router';
import { LoginComponent } from './componentes/login/login.component';
import { InicioComponent } from './componentes/inicio/inicio.component';
import { RecoveryComponent } from './componentes/recovery/recovery.component';
import { MenuPrincipalComponent } from './componentes/menu-principal/menu-principal.component';
import { EnviarMailComponent } from './componentes/enviar-mail/enviar-mail.component';
import { EnviadoComponent } from './componentes/enviado/enviado.component';
import { VerificarcuentaComponent } from './componentes/verificarcuenta/verificarcuenta.component';
import { PerfilComponent } from './componentes/perfil/perfil.component';
import { CuentasComponent } from './componentes/cuentas/cuentas.component';
import { TransaccionesComponent } from './componentes/transacciones/transacciones.component';

export const routes: Routes = [
    {
        path: '',
        component: InicioComponent,
    },

    {
        path: 'login',
        component: LoginComponent,
    },
    {
        path: 'recovery',
        component: RecoveryComponent,
    }, 
    {
        path: 'Menu',
        component: MenuPrincipalComponent,
    },
    {
        path: 'enviarEmail',
        component: EnviarMailComponent,
    },
    {
        path: 'enviado',
        component: EnviadoComponent,
    },
    {
        path: 'VerificarCuenta',
        component: VerificarcuentaComponent,
    },
    {
        path: "perfil",
        component: PerfilComponent,
    },
    {
        path: "cuenta",
        component: CuentasComponent,
    },
    {
        path: "transaccion",
        component: TransaccionesComponent,
    }
];
