import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AllUrlsComponent } from './all-urls/all-urls.component';
import { UrlInfoComponent } from './url-info/url-info.component';

export const routes: Routes = [
  { path: 'info/:id', component: UrlInfoComponent },
  { path: 'login', component: LoginComponent },
  { path: 'urls', component: AllUrlsComponent },
  { path: '', redirectTo: '/urls', pathMatch: 'full' },
];
