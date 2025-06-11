import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Burger, BurgerService } from '../../services/burger.service';

@Component({
  selector: 'app-burger-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './burger-list.component.html',
  styleUrl: './burger-list.component.css'
})
export class BurgerListComponent implements OnInit {
  burgers: Burger[] = []; // On prépare une liste vide pour stocker nos burgers

  constructor(private burgerService: BurgerService) { }

  ngOnInit(): void {
    // Au chargement du composant, on appelle le service
    this.burgerService.getBurgers().subscribe(data => {
      this.burgers = data; // On remplit notre liste avec les données reçues
    });
  }
}
