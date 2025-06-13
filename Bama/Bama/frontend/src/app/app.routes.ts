import { Routes } from '@angular/router';
import { BurgerListComponent } from './components/burger-list/burger-list.component';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { MyAccountComponent } from './pages/my-account/my-account.component';
import { MyOrdersComponent } from './pages/my-orders/my-orders.component';


export const routes: Routes = [
    // Quand l'URL est vide (ex: http://localhost:4200/), on affiche le menu
    { path: '', component: BurgerListComponent },

    // Quand l'URL est /login, on affiche le composant de connexion
    { path: 'login', component: LoginComponent },

    // Quand l'URL est /register, on affiche le composant d'inscription
    { path: 'register', component: RegisterComponent },

    // Quand l'URL est /me, on affiche le composant de mon compte
    { path: 'mon compte', component: MyAccountComponent },

    // Quand l'URL est /mes-commandes, on affiche le composant de mes commandes
    { path: 'mes-commandes', component: MyOrdersComponent },

  ];
