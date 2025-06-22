import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { AuthService } from '../../auth/service/auth.service';
import { IComment } from '../interfaces/IComment';

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  private url = environment.apiUrl+environment.roles.user+environment.routes.comments;
  constructor(
    private httpClient: HttpClient,
    private authService: AuthService
  ) { }

  getComments(userId: string) {
    return this.httpClient.get<IComment[]>(this.url + '/' + userId);
  }

  addComment(comment: any) {
    return this.httpClient.post<IComment>(this.url, comment);
  }

  deleteComment(commentId: string) {
    return this.httpClient.delete(this.url + `/${commentId}`);
  }

  updateComment(comment: any) {
    return this.httpClient.put<IComment>(
      this.url, comment
    );
  }
}
