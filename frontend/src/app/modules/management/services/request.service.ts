import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Request } from '../interfaces/Request';

@Injectable({
  providedIn: 'root'
})
export class RequestService {
  private url = environment.apiUrl + environment.routes.requests

  constructor(private http: HttpClient) { }


  addRequest(data: any) {
    return this.http.post(this.url, data)
  }

  getRequests(filters: Array<{ key: string; value: string[] }>, pageNumber: number, pageSize: number) {
    let params = new HttpParams()
      .append('pageNumber', `${pageNumber}`)
      .append('pageSize', `${pageSize}`);

    filters.forEach(filter => {
      filter.value.forEach(value => {
        params = params.append(filter.key, value);
      });
    });

    return this.http.get<Request[]>(this.url, { params });
  }
}
