import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SkillListService {
  private url = environment.apiUrl+environment.routes.skills;
  constructor(private http: HttpClient) { }

  GetAllSkills() {
    let params = new HttpParams()
      .append(environment.queryParams.pageNumber, 0)
      .append(environment.queryParams.pageSize,0);
    return this.http.get(`${this.url}`, { params })
  }

}
