import { Component, Input } from '@angular/core';
import { TrainModel } from '../models/train.model';

@Component({
  selector: 'app-train-list',
  templateUrl: './train-list.component.html',
  styleUrls: ['./train-list.component.css']
})
export class TrainListComponent {
  @Input() trains!: TrainModel[];
}
