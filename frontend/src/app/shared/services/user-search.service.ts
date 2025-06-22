import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class UserSearchService {
  private url = environment.apiUrl+environment.roles.user+environment.routes.search;

  constructor(private http: HttpClient) { }

  getUserList(currentCount: number, keywords?: any, data?: any) {
    return this.http.get(this.url, {
      params: {
        keywords: keywords.trim(),
        currentCount: currentCount,
        designation: data?.designation ? data?.designation : '',
        experience: data?.experience ? data?.experience : '',
        skills: data?.skills ? data?.skills : '',
        projects: data?.projects ? data?.projects : '',
        certificates: data?.certificates ? data?.certificates : '',
        training: data?.training ? data?.training : ''
      },
    });
  }
}
