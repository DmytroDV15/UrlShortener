import { Component } from '@angular/core';
import { SafeResourceUrl } from '@angular/platform-browser';
import { LoginService } from '../Services/login.service';
import { Router } from '@angular/router';
import { loginModel } from '../Interfaces/loginModel.interface';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
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
    ReactiveFormsModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent {
  loginForm = new FormGroup({
    login: new FormControl('', Validators.required),
    password: new FormControl('', [Validators.required, Validators.minLength(6)]),});

  errorMessage: string = '';

  constructor(
    private loginService: LoginService,
    private router: Router,
  ) {}

  onSubmit() {
    if (this.loginForm.invalid) {
      this.errorMessage = 'Please fill in all fields.';
      return;
    }

    const user: loginModel = { login: this.loginForm.value.login || '', password: this.loginForm.value.password || '' };

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
