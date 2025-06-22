import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { IGiftProgram } from '../interfaces/IGiftProgram';

@Injectable({
  providedIn: 'root',
})


export class GiftProgramService {
  private url = environment.apiUrl+environment.roles.user+environment.routes.giftForms;

  constructor(private http: HttpClient) { }

  postGiftForm(body: any) {
    return this.http.post<IGiftProgram>(this.url, body);
  }

  getGiftDetails() {
    return this.http.get<IGiftProgram[]>(this.url + environment.routes.getAllByUserId);
  }
}
