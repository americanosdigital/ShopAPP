import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'auth/login'
  },
  {
    path: 'auth',
    loadChildren: () => import('./auth/auth.routes').then(m => m.routes),    
  },
  {
    path: 'store',
    loadChildren: () => import('./storefront/store.routes').then(m => m.routes)
  },
  {
    path: 'admin',
    loadChildren: () => import('./admin/admin.routes').then(m => m.routes)
  },
  {
    path: '**',
    redirectTo: 'auth/login'
  }
];
