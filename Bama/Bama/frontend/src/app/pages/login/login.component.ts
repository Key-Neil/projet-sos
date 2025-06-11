import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; // Module pour les formulaires
import { Router } from '@angular/router'; // Pour la redirection
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule], // On importe FormsModule
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  // Modèle pour stocker les données du formulaire
  loginData = {
    username: '',
    password: ''
  };

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit(): void {
    this.authService.login(this.loginData).subscribe({
      next: () => {
        alert('Connexion réussie !');
        this.router.navigate(['/']); // On redirige vers la page d'accueil
      },
      error: (err) => {
        alert(`Erreur de connexion : ${err.error?.message || 'Identifiants incorrects'}`);
      }
    });
  }
}
