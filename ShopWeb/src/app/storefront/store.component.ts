import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-store',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="p-6">
      <h1 class="text-3xl font-bold mb-4">Bem-vindo à Loja!</h1>
      <p>Aqui você verá os produtos em destaque.</p>
    </div>
  `
})
export class StoreComponent {}
