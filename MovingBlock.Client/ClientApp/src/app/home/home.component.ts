import { Component, OnDestroy, OnInit, Inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { HttpClient } from '@angular/common/http';

import { TrainModel } from '../models/train.model';
import { SignalRService } from '../signalr.service';
import { TrainDialogComponent } from "../trains/train-dialog.component";
import { ConfirmationDialogComponent } from '../framework/confirmation-dialog.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit, OnDestroy {
  private subscription!: Subscription;

  // trains from SignalR
  trains: TrainModel[] = [];

  constructor(private signalRService: SignalRService, public dialog: MatDialog,
                private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  ngOnInit(): void {
    this.subscription = this.signalRService.getObservable().subscribe(trains => {
      this.trains = trains;
    });
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  startTrain(model: TrainModel): void {
    this.http.post(this.baseUrl + 'trainstart', model).subscribe();
  }

  openStartTrains(): void {
    const dialogRef = this.dialog.open(TrainDialogComponent, {
      width: '250px'
    });

    // Get the result from the dialog after it is closed
    dialogRef.afterClosed().subscribe(result => {
      if(result !== undefined)
        this.startTrain(result);
    });
  }

  openClearTrains(): void {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      data: {
        message: 'Clear all Trains?',
      },
    });

    dialogRef.afterClosed().subscribe((response: boolean) => {
      if(response)
        this.http.post(this.baseUrl + 'trainclear', undefined).subscribe();
    });
  }
}
