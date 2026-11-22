import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from '../environments/environment';

@Injectable({ providedIn: 'root' })
export class TrackingService {

  private hubConnection?: signalR.HubConnection;

  startConnection() {
    if (this.hubConnection) {
      return;
    }

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('/trackinghub')
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start()
      .then(() => console.log('SignalR Connected'))
      .catch(err => console.error('SignalR Error:', err));
  }

  onBusLocation(callback: (data: any) => void) {
    this.hubConnection?.on('ReceiveBusLocation', callback);  
  }

  onAlert(callback: (alert: any) => void) {
  this.hubConnection?.on('ReceiveAlert', callback);
}

  stopConnection() {
    if (!this.hubConnection) {
      return;
    }

    this.hubConnection.stop();
    this.hubConnection = undefined;
  }
}
