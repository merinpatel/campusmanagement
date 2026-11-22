import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Bus } from '../components/bus/bus.model';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BusService {


  constructor(private http: HttpClient) {}

  /* GET ALL BUSES */
  getAll() {
    return this.http.get<Bus[]>(`${environment.apiUrl}/Bus`);
  }

  /* CREATE BUS */
  create(bus: Bus) {
    return this.http.post(`${environment.apiUrl}/Bus`, bus);
  }

  /* UPDATE BUS */
  update(bus: Bus) {
    return this.http.put(`${environment.apiUrl}/Bus`, bus);
  }

  /* DELETE BUS */
  delete(busId: number) {
    return this.http.delete(`${environment.apiUrl}/Bus/${busId}`);
  }
}
