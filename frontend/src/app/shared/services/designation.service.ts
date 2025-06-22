import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
@Injectable({
  providedIn: 'root'
})
export class DesginationService {
  private url = environment.apiUrl  + environment.routes.designation;

  constructor(private httpClient: HttpClient) {   }

  getAllDesignation(){
    return this.httpClient.get(this.url,{params:{
      pageNumber:0,
      pageSize:0
    }})
  }
}
