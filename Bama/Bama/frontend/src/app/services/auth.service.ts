import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common'; // On importe cette fonction utilitaire
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5218/api/customers';

  // On injecte PLATFORM_ID pour savoir où s'exécute le code (serveur ou navigateur)
  constructor(
    private http: HttpClient,
    @Inject(PLATFORM_ID) private platformId: Object
  ) { }

  login(credentials: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/auth`, credentials).pipe(
      tap((response: any) => {
        // On ne stocke le token que si on est dans un navigateur
        if (isPlatformBrowser(this.platformId) && response.token) {
          localStorage.setItem('authToken', response.token);
        }
      })
    );
  }

  register(userInfo: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, userInfo);
  }

  logout(): void {
    // On ne modifie le localStorage que si on est dans un navigateur
    if (isPlatformBrowser(this.platformId)) {
      localStorage.removeItem('authToken');
    }
  }

  getToken(): string | null {
    // On ne lit le localStorage que si on est dans un navigateur
    if (isPlatformBrowser(this.platformId)) {
      return localStorage.getItem('authToken');
    }
    return null; // Sur le serveur, on retourne null
  }

  isLoggedIn(): boolean {
    // On ne vérifie que si on est dans un navigateur
    if (isPlatformBrowser(this.platformId)) {
      return this.getToken() !== null;
    }
    return false; // Sur le serveur, l'utilisateur n'est jamais "connecté"
  }
}
