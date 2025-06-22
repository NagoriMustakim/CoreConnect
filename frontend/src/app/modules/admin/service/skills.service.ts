import { environment } from './../../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class SkillsService {
  private url = environment.apiUrl+environment.routes.skills;
  constructor(private http: HttpClient) { }

  GetAllSkills(pageNumber: number, pageSize: number) {
    let params = new HttpParams()
      .append(environment.queryParams.pageNumber, `${pageNumber}`)
      .append(environment.queryParams.pageSize, `${pageSize}`);
    return this.http.get(`${this.url}`, { params })
  }

  AddSkill(data: any){
    return this.http.post(`${this.url}`,data)
  }

  UpdateSkill(data: any){
    return this.http.put(`${this.url}${environment.actions.update}/${data.skillGuid}`,data)
  }

  DeleteSkill(skillId: string){
    return this.http.delete(`${this.url}${environment.actions.delete}/${skillId}`)
  }
}
