import { Component } from '@angular/core';
import { trigger, state, style, animate, transition } from '@angular/animations';

@Component({
  selector: 'app-fade',
  template: `
    <div [@fade]="show" class="fade">
      Fade me in and out
    </div>
    <div class="container">
      <div class="dot" [@move]="state"></div>
    </div>
    <button (click)="toggle()">Toggle</button>
  `,
  styles: [`
    .fade {
      width: 200px;
      height: 200px;
      background-color: green;
    }
    .container {
      height: 100px;
      border: solid black;
    }
    .dot {
      width: 20px;
      height: 20px;
      border-radius: 50%;
      background-color: red;
      position: absolute
    }
  `],
  animations: [
    trigger('fade', [
      state('true', style({ opacity: 1 })),
      state('false', style({ opacity: 0 })),
      transition('true <=> false', animate(2000))
    ]),
    // Define a trigger named 'move' for the dot element
    trigger('move', [
      // Define two states: start and end
      state('start', style({ left: '1000px' })),
      state('end', style({ left: '200px' })),
      // Define a transition from start to end with a duration of 2 seconds
      transition('start <=> end', animate(2000)),
    ]),
  ]
})
export class FadeComponent {
  show = true;
  state = 'end';

  toggle() {
    this.show = !this.show;
    this.state = this.state === 'start' ? 'end' : 'start';
  }
}
