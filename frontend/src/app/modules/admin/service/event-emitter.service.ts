import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EventEmitterService {

  private resetSubject = new Subject<void>();

  constructor() { }

  resetCharts() {
    this.resetSubject.next();
  }

  getResetObservable() {
    return this.resetSubject.asObservable();
  }
}
