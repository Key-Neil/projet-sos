 import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Burger } from './burger.service'; // On importe l'interface Burger

// Interface pour un article de commande reçu de l'API
export interface OrderItem {
  burger: Burger;
  quantity: number;
  price: number;
}

// Interface pour la commande complète
export interface CustomerOrder {
  customerOrderId: number;
  totalPrice: number;
  orderItems: OrderItem[];
}

// ... (l'interface OrderItemPayload ne change pas)
export interface OrderItemPayload {
  burgerId: number;
  quantity: number;
  price: number;
}

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private orderApiUrl = 'http://localhost:5218/api/customers/me/order';

  constructor(private http: HttpClient) { }

  // NOUVELLE MÉTHODE pour récupérer le panier
  getOrder(): Observable<CustomerOrder> {
    // L'intercepteur ajoutera le token automatiquement
    return this.http.get<CustomerOrder>(this.orderApiUrl);
  }

  addItemToOrder(item: OrderItemPayload): Observable<any> {
    // L'URL pour ajouter un item est légèrement différente
    return this.http.post(`${this.orderApiUrl}/items`, [item]);
  }

   finalizeOrder(): Observable<any> {
    // On fait un appel POST à la nouvelle route de finalisation
    // Pas besoin d'envoyer de données, le token suffit pour que le backend sache qui est le client.
    return this.http.post(`${this.orderApiUrl}/finalize`, {});
  }

  clearOrder(): Observable<any> {
    // On fait un appel DELETE à la route qui gère les articles
    return this.http.delete(`${this.orderApiUrl}/items`);
  }

}
