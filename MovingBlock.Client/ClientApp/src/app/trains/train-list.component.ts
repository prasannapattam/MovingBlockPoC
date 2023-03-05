import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { SpeedDialogComponent } from "./speed-dialog.component";
import { TrainModel } from '../models/train.model';

@Component({
  selector: 'app-train-list',
  templateUrl: './train-list.component.html',
  styleUrls: ['./train-list.component.css']
})
export class TrainListComponent {
  @Input() trains!: TrainModel[];
  @Output() adjustSpeedEvent = new EventEmitter<TrainModel>();

  constructor(public dialog: MatDialog) { }

  adjustSpeed(train: TrainModel): void {
    const dialogRef = this.dialog.open(SpeedDialogComponent, {
      width: "250px",
      data: train
    });

    // Get the result from the dialog after it is closed
    dialogRef.afterClosed().subscribe((result) => {
      if (result !== undefined)
        this.adjustSpeedEvent.emit(result);
    });
  }
}
