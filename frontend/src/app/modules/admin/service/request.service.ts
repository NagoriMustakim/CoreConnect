import { environment } from './../../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
@Injectable({
  providedIn: 'root',
})
export class RequestService {
  private url = environment.apiUrl + environment.routes.requests;

  constructor(private http: HttpClient) { }

  getAllRequests(filters: Array<{ key: string; value: string[] }>, pageNumber: number, pageSize: number) {
    let params = new HttpParams()
      .append(environment.queryParams.pageNumber, `${pageNumber}`)
      .append(environment.queryParams.pageSize, `${pageSize}`);

    filters.forEach(filter => {
      filter.value.forEach(value => {
        params = params.append(filter.key, value);
      });
    });

    return this.http.get(`${this.url}${environment.roles.admin}`, { params });
  }

  ApproveRequest(value: any, requestGuid: string) {
    return this.http.put(
      this.url + '/' + requestGuid,
      value
    );
  }

  RejectRequest(value: any, id: string) {
    return this.http.put(this.url + '/' + id, value);
  }

  DeleteRequest(id: string) {
    return this.http.delete(this.url + '/' + id);
  }
}
