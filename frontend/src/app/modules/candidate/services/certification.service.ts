import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Certification } from '../interfaces/Certification';
import { NzUploadFile } from 'ng-zorro-antd/upload';
import { AuthService } from '../../auth/service/auth.service';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class CertificationService {
  private userId: string = '';

  private url =
    environment.apiUrl +
    environment.roles.user +
    environment.routes.certificates;

  constructor(
    private httpClient: HttpClient,
    private authService: AuthService
  ) { }

  getCertifications(userId: string) {
    if (userId === '') {
      this.userId = this.authService.getUserId();
      return this.httpClient.get<Certification[]>(
        this.url + environment.routes.getAll + '/' + this.userId
      );
    } else {
      return this.httpClient.get<Certification[]>(
        this.url + environment.routes.getAll + '/' + userId
      );
    }
  }

  addCertification(certification: any, fileList: NzUploadFile[]) {
    const formData = new FormData();
    formData.append('certificationGuid', certification.certificationGuid);
    formData.append(
      'companyGuid',
      certification.companyGuid
    );
    formData.append('skillGuid', certification.skillGuid);
    formData.append(
      'certificationIssueDate',
      certification.certificationIssueDate
        ? new Date(certification.certificationIssueDate).toISOString()
        : ''
    );
    formData.append(
      'certificationExpirationDate',
      certification.certificationExpirationDate
        ? new Date(certification.certificationExpirationDate).toISOString()
        : ''
    );
    formData.append(
      'certificationCredentialId',
      certification.certificationCredentialId
    );
    formData.append(
      'certificationCredentialUrl',
      certification.certificationCredentialUrl
    );
    formData.append(
      'certificationDescription',
      certification.certificationDescription
    );

    fileList.forEach((file: any) => {
      formData.append('certificationPhoto', file);
    });

    return this.httpClient.post<Certification>(this.url, formData);
  }

  deleteCertification(certificationId: string) {
    return this.httpClient.delete(this.url + `/${certificationId}`);
  }

  // deleteCertificateImage(certificationId: string){
  //   return this.httpClient.delete(this.url+ )
  // }

  updateCertification(certification: any, fileList: NzUploadFile[]) {
    const formData = new FormData();
    formData.append('userCertificationGuid', certification.userCertificationGuid);
    formData.append('certificationGuid', certification.certificationGuid);
    formData.append(
      'companyGuid',
      certification.companyGuid
    );
    formData.append(
      'skillGuid',
      certification.skillGuid ? certification.skillGuid : ''
    );
    formData.append(
      'certificationIssueDate',
      certification.certificationIssueDate
        ? new Date(certification.certificationIssueDate).toISOString()
        : ''
    );
    formData.append(
      'certificationExpirationDate',
      certification.certificationExpirationDate
        ? new Date(certification.certificationExpirationDate).toISOString()
        : ''
    );
    formData.append(
      'certificationCredentialId',
      certification.certificationCredentialId
        ? certification.certificationCredentialId
        : ''
    );
    formData.append(
      'certificationCredentialUrl',
      certification.certificationCredentialUrl
        ? certification.certificationCredentialUrl
        : ''
    );
    formData.append(
      'certificationDescription',
      certification.certificationDescription
        ? certification.certificationDescription
        : ''
    );
    formData.append(
      'certificationPhotoName',
      certification.certificationPhotoName
    );

    fileList.forEach((file: any) => {
      formData.append('certificationPhoto', file);
    });

    return this.httpClient.put<Certification>(
      this.url + `/${certification.userCertificationGuid}`,
      formData
    );


  }
}
