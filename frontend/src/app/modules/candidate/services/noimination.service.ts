import { AuthService } from './../../auth/service/auth.service';
import { Nomination } from './../interfaces/Nomination';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class NoiminationService {
  private url = environment.apiUrl + environment.routes.nominations;
  private userId: string = '';
  constructor(private http: HttpClient, private authService: AuthService) {}

  addNomination(data: any, userId: string) {
    if (userId === '') {
      data.nomineeGuid = this.authService.getUserId();
      return this.http.post<Nomination>(this.url, data);
    } else {
      data.nomineeGuid = userId;
      return this.http.post<Nomination>(this.url, data);
    }
  }
  getNominations() {
    this.userId = this.authService.getUserId();
    return this.http.get<Nomination[]>(this.url + environment.routes.getAll+'/' + this.userId);
  }
}
