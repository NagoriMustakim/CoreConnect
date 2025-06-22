import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { IGiftProgram } from '../interfaces/IGiftProgram';
import { AuthService } from '../../auth/service/auth.service';

@Injectable({
  providedIn: 'root',
})
export class GiftProgramService {
  private url =
    environment.apiUrl + environment.roles.user + environment.routes.giftForms;

  constructor(private http: HttpClient, private authService: AuthService) { }

  getAllGiftDetails(filters: Array<{ key: string; value: string[] }>, pageNumber: number, pageSize: number) {
    let params = new HttpParams()
      .append('pageNumber', `${pageNumber}`)
      .append('pageSize', `${pageSize}`);

    filters.forEach(filter => {
      filter.value.forEach(value => {
        params = params.append(filter.key, value);
      });
    });

    return this.http.get<IGiftProgram[]>(this.url + environment.routes.getAll, { params });
  }

  getGiftFormById(giftProgramId: any, userId: any) {
    return this.http.get<IGiftProgram>(
      this.url + environment.roles.admin + '/' + userId + '/' + giftProgramId
    );
  }

  putGiftFormStatus(body: any, userId: any, giftProgramId: any) {
    let reviewerId = this.authService.getUserId();

    return this.http.put(this.url + '/' + userId + '/' + giftProgramId, { ...body, giftformReviewerId: reviewerId });
  }
}
