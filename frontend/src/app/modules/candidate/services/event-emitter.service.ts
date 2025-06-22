import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EventEmitterService {

  private resetSubject = new Subject<void>();

  constructor() { }

  refreshProfile() {
    this.resetSubject.next();
  }

  getObservable() {
    return this.resetSubject.asObservable();
  }
}
