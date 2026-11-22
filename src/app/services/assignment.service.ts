import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';

@Injectable({ providedIn: 'root' })
export class AssignmentService {


  constructor(private http: HttpClient) {}

  assignDriver(busId: number, driverId: number) {
    return this.http.post(`${environment.apiUrl}/Assignment/AssignDriver`, {
      busId,
      driverId
    });
  }

  assignStudent(busId: number, studentId: number) {
    return this.http.post(`${environment.apiUrl}/Assignment/AssignStudent`, {
      busId,
      studentId
    });
  }

  removeDriver(driverId: number) {
    return this.http.post(`${environment.apiUrl}/Assignment/RemoveDriver`, { driverId });
  }

  removeStudent(studentId: number) {
    return this.http.post(`${environment.apiUrl}/Assignment/RemoveStudent`, { studentId });
  }
}
