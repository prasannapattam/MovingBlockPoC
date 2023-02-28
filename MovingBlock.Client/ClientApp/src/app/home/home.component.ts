import { Component, OnDestroy, OnInit, Inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { HttpClient } from '@angular/common/http';

import { TrainModel } from '../models/train.model';
import { SignalRService } from '../signalr.service';
import { TrainDialogComponent } from "../trains/train-dialog.component";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit, OnDestroy {
  private subscription!: Subscription;

  message: string = "";
  // sample data
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

  openDialog(): void {
    const dialogRef = this.dialog.open(TrainDialogComponent, {
      width: '250px'
    });

    // Get the result from the dialog after it is closed
    dialogRef.afterClosed().subscribe(result => {
      if(result !== undefined)
        this.startTrain(result);
    });
  }
}
