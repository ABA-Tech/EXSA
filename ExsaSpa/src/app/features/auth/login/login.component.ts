import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';

import { AuthService } from '../../../core/auth/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    CardModule,
    InputTextModule,
    PasswordModule,
    ButtonModule,
    ToastModule
  ],
  providers: [MessageService],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  private readonly fb = inject(FormBuilder);
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);
  private readonly messageService = inject(MessageService);

  loading = false;

  readonly form = this.fb.nonNullable.group({
    identifiant: ['', [Validators.required]],
    motDePasse: ['', [Validators.required]]
  });

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading = true;

    this.authService.login({
      identifiant: this.form.controls.identifiant.value,
      motDePasse: this.form.controls.motDePasse.value,
      tokenFcm: null
    }).subscribe({
      next: () => {
        this.router.navigateByUrl('/dashboard');
      },
      error: (error: unknown) => {
        this.loading = false;

        let detail = 'Connexion impossible. Vérifiez vos identifiants.';

        if (error instanceof HttpErrorResponse) {
          detail = error.error?.message ?? detail;
        }

        this.messageService.add({
          severity: 'error',
          summary: 'Authentification refusée',
          detail
        });
      },
      complete: () => {
        this.loading = false;
      }
    });
  }

  isInvalid(controlName: 'identifiant' | 'motDePasse'): boolean {
    const control = this.form.controls[controlName];
    return control.invalid && (control.dirty || control.touched);
  }
}
