import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  private url = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getDashboardDetails() {
    return this.http.get(this.url + environment.routes.dashboard);
  }
}
