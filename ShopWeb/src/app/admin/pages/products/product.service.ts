import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ProductFormDto } from './dto/product-form.dto';

export interface Product {
  id: string;
  name: string;
  description: string;
  price: number;
}

@Injectable({ providedIn: 'root' })
export class ProductService {
  private apiUrl = `${environment.apiUrl}/products`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiUrl);
  }

  getById(id: string): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/${id}`);
  }

create(product: ProductFormDto): Observable<Product> {
  return this.http.post<Product>(this.apiUrl, product);
}

update(id: string, product: ProductFormDto & { id: string }): Observable<void> {
  return this.http.put<void>(`${this.apiUrl}/${id}`, product);
}

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
