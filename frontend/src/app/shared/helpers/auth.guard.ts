import { AuthService } from './../../modules/auth/service/auth.service';
import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
   const router = inject(Router);
   const authService = inject(AuthService);

   if (typeof window === 'undefined') {
      return false;
   }

   const token = window.localStorage.getItem('token');

   if (token == null) {
      router.navigate(['/auth']);
      return false;
   }

   const expiryFormToken = authService.getExpiry();
   const expiryInDateFormat = new Date(expiryFormToken * 1000);
   if (expiryInDateFormat <= new Date()) {
      router.navigate(['/auth']);
      return false;
   }

   const requiredRoles = route.data['role'];
   const userRole = authService.getRole();

   if (requiredRoles && !requiredRoles.includes(userRole)) {
      router.navigate(['/auth']);
      return false;
   }

   return true;
};
