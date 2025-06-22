import { environment } from './../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBasicDetail } from '../interfaces/IBasicDetail';
import { AuthService } from '../../auth/service/auth.service';

@Injectable({
  providedIn: 'root',
})
export class BasicDetailService {
  private userId: string = '';

  private url =
    environment.apiUrl +
    environment.roles.user +
    environment.routes.basicDetails;

  constructor(
    private httpClient: HttpClient,
    private authService: AuthService
  ) { }

  getBasicDetails() {    
    this.userId = this.authService.getUserId();
    return this.httpClient.get<IBasicDetail>(this.url + '/' + this.userId);
  }
  getUBasicDetails(userId: string) {
    return this.httpClient.get<IBasicDetail>(this.url + '/' + userId);
  }
  updateBasicDetail(basicDetail: any) {
    return this.httpClient.put<IBasicDetail>(
      this.url + `/${basicDetail.id}`,
      basicDetail
    );
  }

  updateAvatar(fileToUpload: any) {
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    return this.httpClient.put(this.url + environment.routes.avatar, formData);
  }


  deleteAvatar() {
    return this.httpClient.delete(this.url + environment.routes.deleteavatar);
  }
}
