import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; // Module pour les formulaires
import { Router } from '@angular/router'; // Pour la redirection
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule], // On importe FormsModule
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  // Modèle pour stocker les données du formulaire d'inscription
  registerData = {
    username: '',
    password: '',
    confirmPassword: ''
  };

  constructor(private authService: AuthService, private router: Router) { }

  onSubmit(): void {
    // Petite vérification pour s'assurer que les mots de passe correspondent
    if (this.registerData.password !== this.registerData.confirmPassword) {
      alert("Les mots de passe ne correspondent pas !");
      return;
    }

    this.authService.register(this.registerData).subscribe({
      next: () => {
        alert('Inscription réussie ! Vous pouvez maintenant vous connecter.');
        this.router.navigate(['/login']); // On redirige vers la page de connexion
      },
      error: (err) => {
        alert(`Erreur d'inscription : ${err.error?.message || 'Une erreur est survenue.'}`);
      }
    });
  }
}
