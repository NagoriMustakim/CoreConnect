import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class InternalProgramCategoriesService {

  private url = environment.apiUrl + environment.routes.internalprogramcategory;

  constructor(private http: HttpClient) { }

  GetAllInternalProgramCategory(pageNumber: number, pageSize: number) {
    let params = new HttpParams()
      .append(environment.queryParams.pageNumber, `${pageNumber}`)
      .append(environment.queryParams.pageSize, `${pageSize}`);
    return this.http.get(`${this.url}`, { params })
  }

  GetCategoriesByInternalProgramId(internalProgramGuid: string) {
    return this.http.get(`${this.url}/${internalProgramGuid}`);
  }

  AddInternalProgramCategory(data: any) {
    return this.http.post(`${this.url}`, data)
  }

  UpdateInternalProgramCategory(data: any) {
    return this.http.put(`${this.url}/${data.internalProgramCategoryGuid}`, data)
  }

  DeleteInternalProgramCategory(internalProgramCategoryId: string) {
    return this.http.delete(`${this.url}/${internalProgramCategoryId}`)
  }

}
