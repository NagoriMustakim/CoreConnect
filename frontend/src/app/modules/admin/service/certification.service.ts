import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Certification } from '../../candidate/interfaces/Certification';

@Injectable({
  providedIn: 'root'
})
export class CertificationService {

  private url = environment.apiUrl + environment.routes.certifications;
  constructor(
    private httpClient: HttpClient,
  ) { }

  getCertifications(pageNumber: number, pageSize: number) {
    let params = new HttpParams()
      .append(environment.queryParams.pageNumber, `${pageNumber}`)
      .append(environment.queryParams.pageSize, `${pageSize}`);

    return this.httpClient.get(this.url, { params });
  }

  addCertification(certification: any) {
    return this.httpClient.post<Certification>(this.url, certification);
  }

  updateCertification(certification: any) {
    return this.httpClient.put<Certification>(this.url + `/${certification.certificationId}`, certification);
  }

  deleteCertification(certificationId: string) {
    return this.httpClient.delete<Certification>(this.url + `/${certificationId}`);
  }
}
