import { AuthService } from './../../auth/service/auth.service';
import { Kra } from '../interfaces/Kra';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class KraService {
  private url =
    environment.apiUrl + environment.roles.user + environment.routes.kras;
  private userId: string = '';
  constructor(private http: HttpClient, private authService: AuthService) {}

  addKra(data: any) {
    return this.http.post<Kra>(this.url, data);
  }

  getKras(userId: string | null) {
    if (userId != '') {
      return this.http.get<Kra[]>(
        this.url + `${environment.routes.getAll}/${this.userId}`
      );
    } else {
      return this.http.get<Kra[]>(
        this.url + `${environment.routes.getAll}/` + userId
      );
    }
  }

  updateKra(data: any) {
    return this.http.put<Kra>(`${this.url}/${data.kraguid}`, data);
  }

  deleteKra(kraguid: string) {
    return this.http.delete(this.url + `/${kraguid}`);
  }
}
