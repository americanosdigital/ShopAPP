import { bootstrapApplication } from '@angular/platform-browser';
import { importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http'; 

import { AppComponent } from './app/app.component';
import { routes } from './app/app.routes';
import { HttpClientModule } from '@angular/common/http';
import { AuthInterceptor } from './app/core/interceptors/auth.interceptor';

import { LucideAngularModule } from 'lucide-angular';
import { User, Package, ShoppingCart, DollarSign } from 'lucide';

bootstrapApplication(AppComponent, {
  providers: [    
    provideRouter(routes),
    provideHttpClient(withInterceptors([AuthInterceptor])),
    importProvidersFrom(
      HttpClientModule,
      LucideAngularModule.pick({ User, Package, ShoppingCart, DollarSign })
    )
  ]
}).catch(err => console.error(err));
