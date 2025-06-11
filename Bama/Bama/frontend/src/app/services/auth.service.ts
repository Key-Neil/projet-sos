import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5218/api/customers';

  constructor(private http: HttpClient) { }

  login(credentials: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/auth`, credentials).pipe(
      tap((response: any) => {
        // Si la connexion réussit, on stocke le token
        if (response.token) {
          localStorage.setItem('authToken', response.token);
        }
      })
    );
  }

  register(userInfo: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, userInfo);
  }

  // Méthode pour se déconnecter
  logout(): void {
    localStorage.removeItem('authToken');
  }

  // Méthode pour récupérer le token
  getToken(): string | null {
    return localStorage.getItem('authToken');
  }

  // Méthode pour savoir si l'utilisateur est connecté
  isLoggedIn(): boolean {
    return this.getToken() !== null;
  }
}
