import { Injectable } from '@angular/core';
import { jwtDecode } from "jwt-decode";
@Injectable({
  providedIn: 'root',
})
export class RoleService {
  getRoles() {
    const token = localStorage.getItem('token');
    if (!token) {
      return [];
    }

    try {

      const decodedToken: any = jwtDecode(token);

      return (
        [decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']] || []
      );
    } catch (error) {
      console.error('Error decoding token', error);
      return [];
    }
  }
}
