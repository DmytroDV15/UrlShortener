import { Component, OnInit } from '@angular/core';
import { AllUrlsComponent } from './all-urls/all-urls.component';
import { LoginComponent } from './login/login.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    RouterOutlet,
    RouterLink,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'T';

  constructor(private router: Router) {}

  onLoginClick() {
    const userId = sessionStorage.getItem('userID');
    if (userId) {
      sessionStorage.removeItem('userID');
    }

    this.router.navigate(['/login']);
  }
}
