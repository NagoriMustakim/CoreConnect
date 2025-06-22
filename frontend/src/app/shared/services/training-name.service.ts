import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TrainingNameService {
  private url = environment.apiUrl + environment.roles.admin + environment.routes.trainings + environment.routes.getAll;
  constructor(private httpClient: HttpClient) { }

  getAllTrainings() {
    return this.httpClient.get(this.url)
  }
}
