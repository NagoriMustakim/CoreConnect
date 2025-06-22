import { AuthService } from './../../auth/service/auth.service';
import { Language } from '../interfaces/Language';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class LanguageService {
  private url =
    environment.apiUrl + environment.roles.user + environment.routes.languages;

  private userId: string = '';

  constructor(private http: HttpClient, private authService: AuthService) {}

  getLanguages(userId: string) {
    if (userId == '') {
      this.userId = this.authService.getUserId();
    } else {
      this.userId = userId;
    }
    return this.http.get<Language[]>(
      this.url + environment.routes.getAll + '/' + this.userId
    );
  }

  addLanguage(data: any) {
    return this.http.post<Language>(this.url, data);
  }

  updateLanguage(data: any) {
    return this.http.put<Language>(`${this.url}/${data.languageGuid}`, data);
  }

  deleteLanguage(languageGuid: string) {
    return this.http.delete(this.url + `/${languageGuid}`);
  }
}
