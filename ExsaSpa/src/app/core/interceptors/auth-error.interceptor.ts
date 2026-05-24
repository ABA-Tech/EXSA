import { inject } from '@angular/core';
import {
  HttpErrorResponse,
  HttpInterceptorFn
} from '@angular/common/http';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { AuthStateService } from '../auth/auth-state.service';

export const authErrorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const authState = inject(AuthStateService);

  return next(req).pipe(
    catchError((error: unknown) => {
      if (error instanceof HttpErrorResponse) {
        const isLoginRequest = req.url.includes('/auth/login');
        const isMeRequest = req.url.includes('/auth/me');

        if (error.status === 401 && !isLoginRequest && !isMeRequest) {
          authState.clear();
          router.navigateByUrl('/login');
        }

        if (error.status === 403) {
          router.navigateByUrl('/access-denied');
        }
      }

      return throwError(() => error);
    })
  );
};
