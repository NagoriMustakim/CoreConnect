import { environment } from './../../../../environments/environment';
import { ITraining } from '../../candidate/interfaces/ITraining';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TrainingService {
  private url =
    environment.apiUrl + environment.roles.user + environment.routes.trainings;
  constructor(private http: HttpClient) {}

  getTrainings(userId: string) {
    return this.http.get<ITraining[]>(
      this.url + environment.routes.getAll + '/' + userId
    );
  }
  addTraining(userId: string, data: any) {
    return this.http.post<ITraining>(
      this.url + environment.roles.admin + '/' + userId,
      data
    );
  }
  updateTraining(userId: string, data: any) {
    return this.http.put<ITraining>(
      this.url +
        environment.roles.admin +
        environment.actions.update +
        '/' +
        userId +
        '/' +
        data.userTrainingGuid,
      data
    );
  }
  deleteTraining(userId: string, trainingTypeGuid: string) {
    return this.http.delete<ITraining>(
      this.url +
        environment.roles.admin +
        environment.actions.delete +
        '/' +
        userId +
        '/' +
        trainingTypeGuid
    );
  }
}
