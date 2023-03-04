import { Component, OnDestroy, OnInit, Inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { HttpClient } from '@angular/common/http';

import { SignalRService } from '../signalr.service';
import { TrainDialogComponent } from "../trains/train-dialog.component";
import { ConfirmationDialogComponent } from '../framework/confirmation-dialog.component';

import { TrainModel } from '../models/train.model';
import { SectionModel } from '../models/section.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit, OnDestroy {
  private subscription!: Subscription;

  section: SectionModel = <SectionModel>{};
  trains: TrainModel[] = [];

  dummyTrain: TrainModel = <TrainModel>{
    trainNumber: 123,
    trainName: "PPK",
    speed: 100,
    trainLength: 600
  }

  constructor(private signalRService: SignalRService, public dialog: MatDialog,
                private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

    http.get<SectionModel>(baseUrl + 'api/section').subscribe(response => {
      // coverting to km & kmph
      response.length = response.length / 1000;
      response.criticalDistance = response.criticalDistance / 1000;
      response.safeDistance = response.safeDistance / 1000;
      response.speed = response.speed * 18 / 5.0;
      this.section = response;
    });
  }

  ngOnInit(): void {
    this.subscription = this.signalRService.getObservable().subscribe((trains: TrainModel[]) => {
      trains.forEach(train => {
        train.speed = train.speed * (18.0 / 5);
      });
      this.trains = trains;
    });
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  startTrain(model: TrainModel): void {
    model.speed = model.speed * (5.0 / 18);
    this.http.post(this.baseUrl + 'simulator/createtraintwin', model).subscribe();
  }

  openStartTrains(): void {
    const dialogRef = this.dialog.open(TrainDialogComponent, {
      width: '250px',
      data: this.dummyTrain
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
        this.http.post(this.baseUrl + 'simulator/cleartraintwins', undefined).subscribe();
    });
  }
}
