import { environment } from './../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AdminDocumentConstants } from '../constants/AdminDocumentConstants';

@Injectable({
 providedIn: 'root',
})
export class ManageUserService {
 readonly baseURI = `${environment.apiUrl}${environment.routes.users}`;
 manageUserModel!: FormGroup;
 editManageUserModel!: FormGroup;
 isEditMode = false;

 constructor(private fb: FormBuilder, private http: HttpClient) {
    this.editManageUserModel = this.fb.group({});
 }

 getAllUsers(pageNumber: number, pageSize: number): Observable<any> {
    const queryParams = new URLSearchParams({
      [environment.queryParams.pageNumber]: pageNumber.toString(),
      [environment.queryParams.pageSize]: pageSize.toString(),
    }).toString();
    return this.http.get(`${this.baseURI}?${queryParams}`);
 }

 deleteUser(id: string): Observable<any> {
    return this.http.delete(`${this.baseURI}/${id}`);
 }

 getUserData(): Observable<any> {
    const userId = localStorage.getItem(AdminDocumentConstants.USERID);
    if (!userId) {
      throw new Error(AdminDocumentConstants.ERROR_USER_ID_NOT_FOUND_IN_LOCAL_STORAGE);
    }
    return this.http.get(`${this.baseURI}/${userId}`);
 }
}
