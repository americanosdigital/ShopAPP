import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="p-6">
      <h1 class="text-3xl font-bold mb-4">Bem-vindo admin-dashboard!</h1>
      <p>Aqui você verá os admin-dashboard.component em destaque.</p>
    </div>
  `
})
export class admindashboardcomponent {}