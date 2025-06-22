import { environment } from './../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBasicDetail } from '../interfaces/IBasicDetail';
import { AdminFormdataConstants } from '../constants/AdminFormdataConstants';

@Injectable({
  providedIn: 'root',
})
export class BasicDetailService {
  private url = environment.apiUrl + environment.roles.user + environment.routes.basicDetails;

  constructor(private http: HttpClient) { }

  getAdminBasicDetails() {
    return this.http.get<IBasicDetail>(this.url + environment.roles.admin);
  }

  getUserBasicDetails(userId: string) {
    return this.http.get(this.url + environment.roles.admin + '/' + userId);
  }

  getUBasicDetails(userId: string) {
    return this.http.get(this.url + '/' + userId);
  }

  updateuserBasicDetails(data: any, userId: string) {
    return this.http.put(this.url + environment.roles.admin + '/' + userId, data);
  }

  updateAvatar(fileToUpload: any, userId: string) {
    const formData = new FormData();
    formData.append(AdminFormdataConstants.FILE, fileToUpload, fileToUpload.name);

    return this.http.put(this.url + environment.roles.admin + environment.routes.avatar + '/' + userId, formData);
  }

  deleteAvtar(userId: string) {
    return this.http.delete(this.url + environment.roles.admin + environment.routes.deleteavatar + '/' + userId);
  }
}
