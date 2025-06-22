import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { AuthService } from '../../auth/service/auth.service';
import { ITraining } from '../interfaces/ITraining';

@Injectable({
  providedIn: 'root',
})
export class TrainingService {
  private url =
    environment.apiUrl + environment.roles.user + environment.routes.trainings;
  private userId: string = '';
  constructor(
    private httpClient: HttpClient,
    private authService: AuthService
  ) {}

  getTrainings(userId: string) {
    if (userId === '') {
      this.userId = this.authService.getUserId();
      return this.httpClient.get<ITraining[]>(
        this.url + environment.routes.getAll + '/' + this.userId
      );
    } else {
      return this.httpClient.get<ITraining[]>(
        this.url + environment.routes.getAll + '/' + userId
      );
    }
  }

  addTraining(training: any) {
    return this.httpClient.post<ITraining>(this.url, training);
  }

  deleteTraining(trainingId: string) {
    return this.httpClient.delete(this.url + `/${trainingId}`);
  }

  updateTraining(training: any) {
    return this.httpClient.put<ITraining>(
      this.url + `/${training.userTrainingGuid}`,
      training
    );
  }
}
