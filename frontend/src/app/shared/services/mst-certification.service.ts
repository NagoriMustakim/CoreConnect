import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MstCertificationService {
  private url = environment.apiUrl + environment.routes.certifications;
  constructor(
    private httpClient: HttpClient,
  ) { }

  getAllCertifications() {
    let params = new HttpParams()
      .append(environment.queryParams.pageNumber, 0)
      .append(environment.queryParams.pageSize, 0);

    return this.httpClient.get(this.url, { params });
  }
}
