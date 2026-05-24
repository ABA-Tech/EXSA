import { HttpInterceptorFn } from '@angular/common/http';
import { environment } from '../../../environments/environment';

export const credentialsInterceptor: HttpInterceptorFn = (req, next) => {
  const isApiRequest =
    req.url.startsWith(environment.apiBaseUrl) ||
    req.url.startsWith('/api');

  if (!isApiRequest) {
    return next(req);
  }

  return next(
    req.clone({
      withCredentials: true
    })
  );
};
