import { environment } from './../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NzUploadFile } from 'ng-zorro-antd/upload';
import { ICertification } from '../interfaces/ICertification';
import { AdminFormdataConstants } from '../constants/AdminFormdataConstants';

@Injectable({
  providedIn: 'root',
})
export class CertificationsService {
  private url =
    environment.apiUrl +
    environment.roles.user +
    environment.routes.certificates;
  constructor(private http: HttpClient) { }

  getCertificates(userId: string) {
    return this.http.get<ICertification[]>(this.url + environment.routes.getAll + '/' + userId);
  }
  createCertificate(
    userId: string,
    certification: any,
    fileList: NzUploadFile[]
  ) {
    const formData = new FormData();
    formData.append(AdminFormdataConstants.CERTIFICATION_GUID, certification.certificationGuid);
    formData.append(
      AdminFormdataConstants.CERTIFICATION_ISSUING_ORGANIZATION,
      certification.companyGuid
    );
    formData.append(AdminFormdataConstants.SKILL_GUID, certification.skillGuid);
    formData.append(
      AdminFormdataConstants.CERTIFICATION_ISSUEDATE,
      certification.certificationIssueDate
        ? new Date(certification.certificationIssueDate).toISOString()
        : ''
    );
    formData.append(
      AdminFormdataConstants.CERTIFICATION_EXPIRATIONDATE,
      certification.certificationExpirationDate
        ? new Date(certification.certificationExpirationDate).toISOString()
        : ''
    );
    formData.append(
      AdminFormdataConstants.CERTIFICATION_CREDENTIALID,
      certification.certificationCredentialId
    );
    formData.append(
      AdminFormdataConstants.CERTIFICATION_CREDENTIALURL,
      certification.certificationCredentialUrl
    );
    formData.append(
      AdminFormdataConstants.CERTIFICATION_DESCRIPTION,
      certification.certificationDescription
    );

    fileList.forEach((file: any) => {
      formData.append(AdminFormdataConstants.CERTIFICATION_PHOTO, file);
    });

    return this.http.post<ICertification>(
      this.url + environment.roles.admin + '/' + userId,
      formData
    );
  }
  editCertificate(
    userId: string,
    certification: any,
    fileList: NzUploadFile[]
  ) {
    const formData = new FormData();
    formData.append(AdminFormdataConstants.CERTIFICATION_CERTIFICATIONGUID, certification.userCertificationGuid);
    formData.append(AdminFormdataConstants.CERTIFICATION_GUID, certification.certificationGuid);
    formData.append(AdminFormdataConstants.CERTIFICATION_ISSUING_ORGANIZATION, certification.companyGuid);
    formData.append(AdminFormdataConstants.SKILL_GUID, certification.skillGuid ? certification.skillGuid : '');
    formData.append(AdminFormdataConstants.CERTIFICATION_ISSUEDATE, certification.certificationIssueDate ? new Date(certification.certificationIssueDate).toISOString() : '');
    formData.append(AdminFormdataConstants.CERTIFICATION_EXPIRATIONDATE, certification.certificationExpirationDate ? new Date(certification.certificationExpirationDate).toISOString() : '');
    formData.append(AdminFormdataConstants.CERTIFICATION_CREDENTIALID, certification.certificationCredentialId ? certification.certificationCredentialId : '');
    formData.append(AdminFormdataConstants.CERTIFICATION_CREDENTIALURL, certification.certificationCredentialUrl ? certification.certificationCredentialUrl : '');
    formData.append(AdminFormdataConstants.CERTIFICATION_DESCRIPTION, certification.certificationDescription ? certification.certificationDescription : '');
    formData.append(AdminFormdataConstants.CERTIFICATION_PHOTONAME, certification.certificationPhotoName);

    fileList.forEach((file: any) => {
      formData.append(AdminFormdataConstants.CERTIFICATION_PHOTO, file);
    });

    return this.http.put<ICertification>(
      this.url +
      environment.roles.admin +
      '/' +
      userId +
      '/' +
      certification.userCertificationGuid,
      formData
    );
  }
  deleteCertificate(userId: string, certificateId: string) {
    return this.http.delete(
      this.url + environment.roles.admin + '/' + userId + '/' + certificateId
    );
  }
}
