import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Driver } from '../components/driver/driver.model';
import { environment } from '../environments/environment';

@Injectable({ providedIn: 'root' })
export class DriverService {


  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get<Driver[]>(`${environment.apiUrl}/driver`);
  }

  create(driver: Driver) {
    return this.http.post(`${environment.apiUrl}/driver`, driver);
  }

  update(driver: Driver) {
    return this.http.put(`${environment.apiUrl}/driver`, driver);
  }

  delete(driverId: number) {
    return this.http.delete(`${environment.apiUrl}/driver/${driverId}`);
  }
}
