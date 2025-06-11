import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginData = {
    username: '',
    password: ''
  };

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit(): void {
    this.authService.login(this.loginData).subscribe({
      next: () => {
        alert('Connexion réussie !');
        this.router.navigate(['/']); // Redirection vers l'accueil
      },
      error: (err) => {
        alert(`Erreur de connexion : Identifiants incorrects ou problème serveur.`);
      }
    });
  }
}
