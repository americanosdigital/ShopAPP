import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { ProductService } from './product.service';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, HttpClientModule],
  template: `
    <div class="p-4">
      <h2 class="text-xl font-bold mb-4">{{ editing ? 'Editar' : 'Novo' }} Produto</h2>
      <form [formGroup]="form" (ngSubmit)="onSubmit()" class="space-y-4">
        <div>
          <label class="block mb-1">Nome</label>
          <input class="border p-2 w-full" formControlName="name" />
        </div>
        <div>
          <label class="block mb-1">Descrição</label>
          <input class="border p-2 w-full" formControlName="description" />
        </div>
        <div>
          <label class="block mb-1">Preço</label>
          <input class="border p-2 w-full" formControlName="price" type="number" />
        </div>
        <div>
          <label class="block mb-1">Categoria</label>
          <select class="border p-2 w-full" formControlName="categoryId">
            <option *ngFor="let cat of categories" [value]="cat.id">{{ cat.name }}</option>
          </select>
        </div>
        <button class="bg-green-600 text-white px-4 py-2 rounded">Salvar</button>
      </form>
    </div>
  `
})
export class ProductFormComponent {
  private fb = inject(FormBuilder);
  private service = inject(ProductService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private http = inject(HttpClient);

  form = this.fb.group({
    name: ['', Validators.required],
    description: ['', Validators.required],
    price: [0, Validators.required],
    categoryId: ['', Validators.required]
  });

  categories: any[] = [];
  editing = false;
  id: string | null = null;

  ngOnInit() {
    this.http.get<any[]>(`${environment.apiUrl}/productcategories`).subscribe(data => {
      this.categories = data;
    });

    this.id = this.route.snapshot.paramMap.get('id');
    if (this.id && this.id !== 'new') {
      this.editing = true;
      this.service.getById(this.id).subscribe(prod => this.form.patchValue(prod));
    }
  }

  onSubmit() {
    if (this.form.invalid) return;
    const product = this.form.value;
    if (this.editing && this.id) {
      this.service.update(this.id, product).subscribe(() => this.router.navigate(['/admin/products']));
    } else {
      this.service.create(product).subscribe(() => this.router.navigate(['/admin/products']));
    }
  }
}
