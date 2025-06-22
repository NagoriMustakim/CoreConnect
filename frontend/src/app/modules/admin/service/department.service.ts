import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {
  readonly baseURI = environment.apiUrl + environment.routes.department;

  constructor(private http: HttpClient) { }

  getAllDepartment(pageNumber: number, pageSize: number) {
    let params = new HttpParams()
      .append(environment.queryParams.pageNumber, `${pageNumber}`)
      .append(environment.queryParams.pageSize, `${pageSize}`);
    return this.http.get(`${this.baseURI}`, { params })
  }

  getDepartmentByDepartmentId(departmentGuid: string) {
    return this.http.get(`${this.baseURI}/${departmentGuid}`);
  }

  addDepartment(data: any) {
    return this.http.post(`${this.baseURI}`, data)
  }

  updateDepartment(data: any) {
    return this.http.put(`${this.baseURI}`, data)
  }

  deleteDepartment(departmentId: string) {
    return this.http.delete(`${this.baseURI}/${departmentId}`)
  }

}
