import { environment } from './../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class InternalprogramService {
  readonly baseURI = environment.apiUrl+environment.routes.internalprogram;
  constructor(private http: HttpClient) { }
  getInternalPrograms() {
    return this.http.get(this.baseURI);
  }
}
