import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private url = environment.apiUrl + environment.routes.notifications;

  constructor(private httpClient: HttpClient) { }

  getNotifications(userId: string) {
    return this.httpClient.get(`${this.url}/${userId}`);
  }
}
