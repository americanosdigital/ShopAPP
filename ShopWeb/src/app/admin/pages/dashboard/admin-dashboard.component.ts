import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LucideAngularModule } from 'lucide-angular';
import { User, Package, ShoppingCart, DollarSign } from 'lucide';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    LucideAngularModule
  ],
  templateUrl: './admin-dashboard.component.html'
})
export class AdminDashboardComponent {
  icons = {
    User,
    Package,
    ShoppingCart,
    DollarSign
  };
}
