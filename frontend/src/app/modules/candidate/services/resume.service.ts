import { environment } from './../../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ResumeService {
  private baseURI = environment.apiUrl+environment.routes.users;
  constructor(private http: HttpClient) { }

  getResumeData(businessUnitId: any, userId: any) {
    return this.http.get(this.baseURI + "/" + businessUnitId, {
      params: {
        userId: userId
      },
    });
  }
}
