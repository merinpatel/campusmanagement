import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

import { environment} from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  private hubConnection!: signalR.HubConnection;

  start() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(environment.signalRUrl)
      .withAutomaticReconnect()
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('SignalR connected'))
      .catch(err => console.error(err));
  }

  onLocation(callback: (data: any) => void) {
    this.hubConnection.on('ReceiveBusLocation', callback);
  }
}
