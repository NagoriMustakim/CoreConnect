import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  let token = null;
  if (typeof window !== 'undefined') {
    token = localStorage.getItem('token');
  }

  const cloneRequest = req.clone({
    setHeaders: {
      Authorization: `Bearer ${token}`,
    },
  });
  return next(cloneRequest);
};
