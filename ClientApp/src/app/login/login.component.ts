import { Component } from '@angular/core';
import { SafeResourceUrl } from '@angular/platform-browser';
import { LoginService } from '../Services/login.service';
import { Router } from '@angular/router';
import { loginModel } from '../Interfaces/loginModel.interface';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-login',
  imports: [
    FormsModule,
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDividerModule,
    MatIconModule,
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent {
  login: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(
    private loginService: LoginService,
    private router: Router,
  ) {}

  onSubmit() {
    const user: loginModel = { login: this.login, password: this.password };

    this.loginService.login(user).subscribe({
      next: (result) => {
        console.log('Login successful:', result);
        this.router.navigate(['']);
      },
      error: (error) => {
        console.error('Login failed:', error);
        this.errorMessage = 'Invalid login or password';
      },
    });
  }
}
