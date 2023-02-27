import { Injectable, Inject } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  private hubConnection: HubConnection;
  private messageSubject = new Subject<string>();

  constructor(@Inject('BASE_URL') private baseUrl: string) {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(baseUrl + "trainhub")
      .build();

    this.hubConnection.on('timerEvent', (message) => {
      this.messageSubject.next(message);
    });

    this.hubConnection.start();
  }
  getObservable() {
    return this.messageSubject.asObservable();
  }
}
