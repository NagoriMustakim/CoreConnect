import { AuthService } from './../../auth/service/auth.service';
import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { IBasicDetail } from '../interfaces/IBasicDetail';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class BasicDetailService {
  private url =
    environment.apiUrl +
    environment.roles.user +
    environment.routes.basicDetails;
  private userId: string = '';
  constructor(
    private httpClient: HttpClient,
    private authService: AuthService
  ) {}

  getBasicDetails() {
    this.userId = this.authService.getUserId();
    return this.httpClient.get<IBasicDetail>(this.url + '/' + this.userId);
  }

  getUBasicDetails(userId: string) {
    console.log(this.url);
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

  deleteAvatar(){
    return this.httpClient.delete(this.url + environment.routes.deleteavatar);
  }
}
