import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { ITraining } from '../interfaces/ITraining';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TrainingsNamesService {
  private url =
    environment.apiUrl + environment.roles.admin + environment.routes.trainings;
  constructor(
    private httpClient: HttpClient
  ) { }

  getAllTrainings(pageNumber: number, pageSize: number): Observable<any> {
    const queryParams = new URLSearchParams({
      [environment.queryParams.pageNumber]: pageNumber.toString(),
      [environment.queryParams.pageSize]: pageSize.toString(),
    }).toString();
    return this.httpClient.get<ITraining[]>(`${this.url}?${queryParams}`);
  }
  CreateTrainings(data: any) {
    return this.httpClient.post<ITraining>(this.url, data);
  }
  UpdateTrainings(data: any) {
    return this.httpClient.put<ITraining>(this.url, data);
  }
  DeleteTrainings(trainingGuid: string) {
    return this.httpClient.delete(this.url + "/" + trainingGuid);
  }
}
