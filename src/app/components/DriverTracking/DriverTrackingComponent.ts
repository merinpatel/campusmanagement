import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-driver-tracking',
  templateUrl: './DriverTrackingComponent.html',
  imports:[CommonModule,FormsModule]
})
export class DriverTrackingComponent {

  busId!: number;

  isTracking = false;

  watchId: number | null = null;

  constructor(private http: HttpClient) {}

  startTracking() {

    if (!this.busId) {
      alert('Enter Bus ID');
      return;
    }

    this.isTracking = true;

    this.watchId = navigator.geolocation.watchPosition(

      (position) => {

        const payload = {
          busId: this.busId,
          latitude: position.coords.latitude,
          longitude: position.coords.longitude,
          speed: position.coords.speed ?? 0
        };

        console.log(payload);

        this.http.post(
          `${environment.apiUrl}/tracking/location`,
          payload
        ).subscribe();
      },

      (error) => {
        console.error(error);
      },

      {
        enableHighAccuracy: true,
        timeout: 5000,
        maximumAge: 0
      }
    );
  }

  stopTracking() {

    if (this.watchId) {
      navigator.geolocation.clearWatch(this.watchId);
    }

    this.isTracking = false;
  }
}
