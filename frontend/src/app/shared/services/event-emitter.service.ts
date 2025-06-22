import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EventEmitterService {

  private subject = new Subject<void>();

  constructor() { }

  callGetDetails() {
    this.subject.next();
  }

  getObservable() {
    return this.subject.asObservable();
  }
}
