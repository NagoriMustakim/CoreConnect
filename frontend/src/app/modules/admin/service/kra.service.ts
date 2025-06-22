import { environment } from './../../../../environments/environment';
import { AuthService } from './../../auth/service/auth.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Kra } from '../../candidate/interfaces/Kra';

@Injectable({
  providedIn: 'root',
})
export class KraService {
  private url =
    environment.apiUrl + environment.roles.user + environment.routes.kras;
  private userId: string = '';

  GetUserLanguages(id: string) {
    return this.http.get(`${this.url}${environment.routes.getAll}/${id}`);
  }
  constructor(private http: HttpClient, private authService: AuthService) { }

  GetUserKras(id: string) {
    const userId = id || this.authService.getUserId();
    return this.http.get<Kra[]>(`${this.url}${environment.routes.getAll}/${userId}`);
  }

  AddUserKra(data: any, id: string) {
    return this.http.post(`${this.url}${environment.roles.admin}/${id}`, data);
  }

  UpdateUserKra(data: any, id: string) {
    return this.http.put(
      `${this.url}${environment.roles.admin}${environment.actions.update}/${id}/${data.kraguid}`,
      data
    );
  }

  DeleteUserKra(kraguid: string, id: string) {
    return this.http.delete(
      `${this.url}${environment.roles.admin}${environment.actions.delete}/${id}/${kraguid}`
    );
  }
}
