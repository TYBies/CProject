import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from '../../services/auth.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {
    this.registerForm = this.formBuilder.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required]]
    }, { validator: this.passwordMatchValidator });
  }

  ngOnInit(): void {}

  passwordMatchValidator(form: FormGroup) {
    const password = form.get('password');
    const confirmPassword = form.get('confirmPassword');
    return password && confirmPassword && password.value === confirmPassword.value
      ? null : { mismatch: true };
  }

  async onSubmit() {
    if (this.registerForm.valid) {
      const { username, password } = this.registerForm.value;
      try {
        const message = await this.authService.register(username, password);
        this.snackBar.open(message, 'Close', {
          duration: 3000
        });
        this.router.navigate(['/login']);
      } catch (error: unknown) {
        let errorMessage = 'Registration failed. Please try again.';

        if (error instanceof HttpErrorResponse) {
          if (error.status === 409) {
            errorMessage = 'Username already exists.';
          } else if (error.error && typeof error.error === 'string') {
            errorMessage = error.error;
          }
        } else if (error instanceof Error) {
          errorMessage = error.message;
        }

        this.snackBar.open(errorMessage, 'Close', {
          duration: 3000
        });
        console.error('Registration failed', error);
      }
    }
  }
}