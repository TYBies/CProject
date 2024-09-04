// main.ts
import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideRouter } from '@angular/router';
import { routes } from './app/app.routes'; // Import the routes

import { importProvidersFrom } from '@angular/core';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { AppModule } from './app/app.module';

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(routes) // Provide routes to the standalone AppComponent
  ]
}).catch(err => console.error(err));

bootstrapApplication(AppComponent, {
  providers: [
    importProvidersFrom(AppModule), provideAnimationsAsync()
  ]
}).catch(err => console.error(err));