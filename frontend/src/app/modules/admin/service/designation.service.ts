import { Designation } from './../interfaces/designation';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class DesignationService {
  private url = environment.apiUrl + environment.routes.designation;
  constructor(private httpClient: HttpClient) {}

  getDesignations(pageNumber: number, pageSize: number) {
    let params = new HttpParams()
      .append(environment.queryParams.pageNumber, `${pageNumber}`)
      .append(environment.queryParams.pageSize, `${pageSize}`);
    return this.httpClient.get(this.url, { params });
  }

  addDesignation(designation: any) {
    return this.httpClient.post<Designation>(this.url, designation);
  }
  updateDesignation(designation: any) {
    return this.httpClient.put<Designation>(this.url, designation);
  }
  deleteDesignation(designationId: any) {
    return this.httpClient.delete<Designation>(this.url + `/${designationId}`);
  }
}
