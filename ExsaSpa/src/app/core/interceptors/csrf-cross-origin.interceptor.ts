import { HttpInterceptorFn } from '@angular/common/http';
import { environment } from '../../../environments/environment';

const MUTATING_METHODS = ['POST', 'PUT', 'PATCH', 'DELETE'];

function readCookie(name: string): string | null {
  const cookies = document.cookie.split(';');

  for (const cookie of cookies) {
    const [key, ...valueParts] = cookie.trim().split('=');

    if (key === name) {
      return decodeURIComponent(valueParts.join('='));
    }
  }

  return null;
}

export const csrfCrossOriginInterceptor: HttpInterceptorFn = (req, next) => {
  const isApiRequest = req.url.startsWith(environment.apiBaseUrl);
  const isMutatingRequest = MUTATING_METHODS.includes(req.method.toUpperCase());

  if (!isApiRequest || !isMutatingRequest) {
    return next(
      req.clone({
        withCredentials: isApiRequest
      })
    );
  }

  const xsrfToken = readCookie('XSRF-TOKEN');

  return next(
    req.clone({
      withCredentials: true,
      setHeaders: xsrfToken
        ? {
            'X-XSRF-TOKEN': xsrfToken
          }
        : {}
    })
  );
};
