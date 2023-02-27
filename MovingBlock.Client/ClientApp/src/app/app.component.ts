import { Component, OnInit } from '@angular/core';
import { SignalRService } from './signalr.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = "app";
  message: string = "";

  constructor(private signalRService: SignalRService) {
  }

  ngOnInit(): void {
    this.signalRService.getObservable().subscribe(message => {
      this.message = message;
    });
  }
}
