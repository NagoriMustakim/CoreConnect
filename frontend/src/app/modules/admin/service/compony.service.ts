import { environment } from './../../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Company } from '../interfaces/company';

@Injectable({
  providedIn: 'root',
})
export class ComponyService {
  private url = environment.apiUrl + environment.routes.companies;
  constructor(private httpClient: HttpClient) {}

  getComponies(pageNumber: number, pageSize: number) {
    let params = new HttpParams()
      .append(environment.queryParams.pageNumber, `${pageNumber}`)
      .append(environment.queryParams.pageSize, `${pageSize}`);

    return this.httpClient.get(this.url, { params });
  }

  addCompony(compony: any) {
    return this.httpClient.post<Company>(this.url, compony);
  }
  updateCompony(compony: any) {
    return this.httpClient.put<Company>(this.url, compony);
  }
  deleteCompony(componyId: string) {
    return this.httpClient.delete<Company>(this.url + `/${componyId}`);
  }
}
