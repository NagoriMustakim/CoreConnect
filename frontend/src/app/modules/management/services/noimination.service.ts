import { AuthService } from './../../auth/service/auth.service';
import { Nomination } from './../interfaces/Nomination';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { NzUploadFile } from 'ng-zorro-antd/upload';

@Injectable({
  providedIn: 'root',
})
export class NoiminationService {
  private url = environment.apiUrl + environment.routes.nominations;
  private userId: string = '';
  constructor(private http: HttpClient, private authService: AuthService) { }

  addNomination(data: any, userId: string, fileList: NzUploadFile[]) {
    const formData = new FormData();

    formData.append('nomineeGuid', userId);
    formData.append('internalProgramGuid', data.internalProgramGuid);
    formData.append('internalProgramCategoryGuid', data.internalProgramCategoryGuid ? data.internalProgramCategoryGuid : '');
    formData.append('justificationComment', data.justificationComment);
    fileList.forEach((file: any) => {
      formData.append('attachment', file);
    });

    console.log(formData);

    if (userId === '') {
      data.nomineeGuid = this.authService.getUserId();
      return this.http.post<Nomination>(this.url, formData);
    } else {
      data.nomineeGuid = userId;
      return this.http.post<Nomination>(this.url, formData);
    }
  }

  // getNominations() {
  //   this.userId = this.authService.getUserId();
  //   return this.http.get<Nomination[]>(
  //     this.url + environment.routes.getAll + '/' + this.userId
  //   );
  // }

  getNominations(filters: Array<{ key: string; value: string[] }>, pageNumber: number, pageSize: number) {
    let params = new HttpParams()
      .append('pageNumber', `${pageNumber}`)
      .append('pageSize', `${pageSize}`);

    filters.forEach(filter => {
      filter.value.forEach(value => {
        console.log("filter.key, value", filter.key, value);
        params = params.append(filter.key, value);
      });
    });

    return this.http.get<Nomination[]>(this.url, { params });
  }
}
