import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerData = {
    username: '',
    password: '',
    confirmPassword: ''
  };

  constructor(private authService: AuthService, private router: Router) { }

  onSubmit(): void {
    if (this.registerData.password !== this.registerData.confirmPassword) {
      alert("Les mots de passe ne correspondent pas !");
      return;
    }

    this.authService.register(this.registerData).subscribe({
      next: () => {
        alert('Inscription réussie ! Vous pouvez maintenant vous connecter.');
        this.router.navigate(['/login']);
      },
      error: (err) => {
        alert(`Erreur d'inscription : ${err.error?.message || 'Une erreur est survenue.'}`);
      }
    });
  }
}
