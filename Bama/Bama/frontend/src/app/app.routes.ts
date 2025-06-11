import { Routes } from '@angular/router';
import { BurgerListComponent } from './components/burger-list/burger-list.component';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';

export const routes: Routes = [
    // Quand l'URL est vide (ex: http://localhost:4200/), on affiche le menu
    { path: '', component: BurgerListComponent },

    // Quand l'URL est /login, on affiche le composant de connexion
    { path: 'login', component: LoginComponent },

    // Quand l'URL est /register, on affiche le composant d'inscription
    { path: 'register', component: RegisterComponent }
];
