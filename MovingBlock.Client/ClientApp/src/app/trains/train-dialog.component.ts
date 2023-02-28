import { Component, Inject } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-train-dialog',
  templateUrl: './train-dialog.component.html',
})
export class TrainDialogComponent {

  constructor(
    public dialogRef: MatDialogRef<TrainDialogComponent>) { }

  // Close the dialog without passing any data
  onCancel(): void {
    this.dialogRef.close();
  }
}
