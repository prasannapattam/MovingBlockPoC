import { Component, Input, OnInit } from '@angular/core';
import { interval } from 'rxjs';

@Component({
  selector: 'app-railway-path',
  templateUrl: './railway-path.component.html',
  styleUrls: ['./railway-path.component.css']
})
export class RailwayPathComponent implements OnInit {
  pathLength: number = 50; // kms
  pathPixel: number = 2400;
  trainLocation: number = 0;
  speedkmph: number = 1000;
  intervalsecs: number = 1; // seconds

  pixelPerMeter: number = 0;
  speedmps: number = 0;
  intervalmillisec: number = 0;

  ngOnInit(): void {

    this.pixelPerMeter = this.pathPixel / (this.pathLength * 1000)
    this.intervalmillisec = this.intervalsecs * 1000;

    this.moveTrain();
  }

  moveTrain(): void {
    let intervalID = setInterval(() => {
      if (this.trainLocation < this.pathPixel) {
        // distnace travelled
        let distance = (this.speedkmph * 1000 / 3600) * this.intervalsecs;
        this.trainLocation += Math.round(distance * this.pixelPerMeter);
      }
      else
      clearTimeout(intervalID);
    }, this.intervalmillisec);
  }
}
