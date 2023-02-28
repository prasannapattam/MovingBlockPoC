import { Component } from '@angular/core';
import { trigger, state, style, animate, transition } from '@angular/animations';

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html',
  styleUrls: ['./counter.component.css'],
  animations: [
    // Define a trigger named 'move' for the dot element
    trigger('move', [
      // Define two states: start and end
      state('start', style({ left: '0px' })),
      state('end', style({ left: '200px' })),
      // Define a transition from start to end with a duration of 2 seconds
      transition('start => end', animate('2s')),
    ]),
  ],
})
export class CounterComponent {
  // Define a variable to store the current state of the dot element
  state = 'start';

  // Define a method to toggle the state of the dot element
  toggleState() {
    this.state = this.state === 'start' ? 'end' : 'start';

  }
}
