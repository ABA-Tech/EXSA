import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { catchError, finalize, map, Observable, of, switchMap, tap } from 'rxjs';

import { environment } from '../../../environments/environment';
import { AuthStateService } from './auth-state.service';
import { AuthUser, LoginRequest } from './auth.models';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly router = inject(Router);
  private readonly authState = inject(AuthStateService);

  private readonly apiUrl = `${environment.apiBaseUrl}/Auth`;

  getCsrfToken(): Observable<void> {
    return this.http.get<void>(`${this.apiUrl}/csrf`);
  }

  login(request: LoginRequest): Observable<AuthUser> {
    return this.getCsrfToken().pipe(
      switchMap(() =>
        this.http.post<AuthUser>(`${this.apiUrl}/login`, request)
      ),
      tap(user => {
        this.authState.setUser(user);
      })
    );
  }

  loadCurrentUser(): Observable<AuthUser | null> {
    return this.http.get<AuthUser>(`${this.apiUrl}/me`).pipe(
      tap(user => {
        this.authState.setUser(user);
      }),
      catchError(() => {
        this.authState.clear();
        return of(null);
      })
    );
  }

  ensureCurrentUser(): Observable<AuthUser | null> {
    const currentUser = this.authState.user();

    if (currentUser) {
      return of(currentUser);
    }

    return this.loadCurrentUser();
  }

  logout(): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/logout`, {}).pipe(
      finalize(() => {
        this.authState.clear();
        this.router.navigateByUrl('/login');
      })
    );
  }
}
