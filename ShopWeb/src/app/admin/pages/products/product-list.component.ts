import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ProductService, Product } from './product.service';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, RouterModule, HttpClientModule],
  template: `
    <div class="p-4">
      <h2 class="text-xl font-bold mb-4">Produtos</h2>
      <button routerLink="/admin/products/new" class="bg-blue-500 text-white px-4 py-2 rounded mb-4">
        Novo Produto
      </button>
      <table class="min-w-full border text-sm">
        <thead class="bg-gray-200">
          <tr>
            <th class="border px-4 py-2 text-left">Nome</th>
            <th class="border px-4 py-2 text-left">Descrição</th>
            <th class="border px-4 py-2 text-left">Preço</th>
            <th class="border px-4 py-2">Ações</th>
          </tr>
        </thead>
        <tbody>
          <tr *ngFor="let product of products">
            <td class="border px-4 py-2">{{ product.name }}</td>
            <td class="border px-4 py-2">{{ product.description }}</td>
            <td class="border px-4 py-2">{{ product.price | currency }}</td>
            <td class="border px-4 py-2 text-center">
              <button routerLink="/admin/products/{{ product.id }}" class="text-blue-600 hover:underline">
                Editar
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  `
})
export class ProductListComponent {
  private service = inject(ProductService);
  products: Product[] = [];

  ngOnInit() {
    this.service.getAll().subscribe(data => this.products = data);
  }
}
