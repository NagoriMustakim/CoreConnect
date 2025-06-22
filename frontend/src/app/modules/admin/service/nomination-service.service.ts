import { environment } from './../../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NominationServiceService {

  private url = environment.apiUrl + environment.routes.nominations;
  constructor(private http: HttpClient) { }

  getAllNominationById(filters: Array<{ key: string; value: string[] }>, id: string, pageNumber: number, pageSize: number) {
    let params = new HttpParams()
      .append(environment.queryParams.pageNumber, `${pageNumber}`)
      .append(environment.queryParams.pageSize, `${pageSize}`);

    filters.forEach(filter => {
      filter.value.forEach(value => {
        params = params.append(filter.key, value);
      });
    });

    return this.http.get(`${this.url}${environment.routes.getAll}/${id}`, { params });
  }

  DeleteNomination(id: string) {
    return this.http.delete(`${this.url}/${id}`);
  }

  ApproveNomination(id: string) {
    return this.http.put(`${this.url}${environment.actions.approve}/${id}`, null);
  }

  RejectNomination(id: string, body: any) {
    return this.http.put(`${this.url}${environment.actions.reject}/${id}`, { ...body });
  }
}
