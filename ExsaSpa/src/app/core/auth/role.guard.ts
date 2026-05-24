import { inject } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivateFn,
  Router
} from '@angular/router';
import { map } from 'rxjs';

import { AuthService } from './auth.service';
import { Role } from './auth.models';

export const roleGuard: CanActivateFn = (route: ActivatedRouteSnapshot) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const requiredRoles = route.data['roles'] as Role[] | undefined;

  if (!requiredRoles || requiredRoles.length === 0) {
    return true;
  }

  return authService.ensureCurrentUser().pipe(
    map(user => {
      if (!user) {
        return router.createUrlTree(['/login']);
      }

      if (requiredRoles.includes(user.role)) {
        return true;
      }

      return router.createUrlTree(['/access-denied']);
    })
  );
};
