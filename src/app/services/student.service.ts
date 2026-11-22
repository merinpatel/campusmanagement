import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Student } from '../components/student/student.model';
import { environment } from '../environments/environment';

@Injectable({ providedIn: 'root' })
export class StudentService {


  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get<Student[]>(`${environment.apiUrl}/student`);
  }

  create(student: Student) {
    return this.http.post(`${environment.apiUrl}/student`, student);
  }

  update(student: Student) {
    return this.http.put(`${environment.apiUrl}/student`, student);
  }

  delete(studentId: number) {
    return this.http.delete(`${environment.apiUrl}/student/${studentId}`);
  }
}
