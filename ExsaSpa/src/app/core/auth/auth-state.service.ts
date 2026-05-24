import { computed, Injectable, signal } from '@angular/core';
import { AuthUser, Role } from './auth.models';

@Injectable({
  providedIn: 'root'
})
export class AuthStateService {
  private readonly userSignal = signal<AuthUser | null>(null);

  readonly user = this.userSignal.asReadonly();

  readonly isAuthenticated = computed(() => this.userSignal() !== null);

  readonly role = computed(() => this.userSignal()?.role ?? null);

  readonly idUtilisateur = computed(() => this.userSignal()?.idUtilisateur ?? null);

  readonly idLocataire = computed(() => this.userSignal()?.idLocataire ?? null);

  setUser(user: AuthUser): void {
    this.userSignal.set(user);
  }

  clear(): void {
    this.userSignal.set(null);
  }

  hasAnyRole(roles: Role[]): boolean {
    const currentRole = this.userSignal()?.role;
    return !!currentRole && roles.includes(currentRole);
  }
}
