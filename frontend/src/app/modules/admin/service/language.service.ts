import { environment } from './../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LanguageService {
  private url = environment.apiUrl + environment.roles.user+environment.routes.languages
  constructor(private http: HttpClient) { }

  GetUserLanguages(id: string) {
    return this.http.get(`${this.url}${environment.routes.getAll}/${id}`)
  }

  AddUserLanguage(data: any, id: string) {
    return this.http.post(`${this.url}${environment.roles.admin}/${id}`, data)
  }

  UpdateUserLangauge(data: any, id: string) {
    return this.http.put(`${this.url}${environment.roles.admin}${environment.actions.update}/${id}/${data.languageGuid}`, data)
  }

  DeleteUserLangauge(languageGuid: string, id: string) {
    return this.http.delete(`${this.url}${environment.roles.admin}${environment.actions.delete}/${id}/${languageGuid}`)
  }
}
