import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Request } from '../interfaces/Request';

@Injectable({
  providedIn: 'root'
})
export class RequestService {
  private url = environment.apiUrl+environment.routes.requests

  constructor(private http: HttpClient) { }


  addRequest(data: any) {
    return this.http.post(this.url, data)
  }

  UserRequests() {
    return this.http.get<Request[]>(this.url);
  }


}
