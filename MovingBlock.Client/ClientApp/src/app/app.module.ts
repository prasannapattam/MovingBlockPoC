import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';


// Navigation
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';

//Not required
import { FadeComponent } from './counter/fade.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';

// framework
import { ConfirmationDialogComponent } from './framework/confirmation-dialog.component';

// Train
import { SectionComponent } from "./trains/section.component";
import { TrainListComponent } from "./trains/train-list.component";
import { RailwayPathComponent } from "./trains/railway-path.component";
import { TrainDialogComponent } from "./trains/train-dialog.component";
import { SpeedDialogComponent } from "./trains/speed-dialog.component";

//services
import { SignalRService } from './signalr.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,

    FadeComponent,
    CounterComponent,
    FetchDataComponent,

    ConfirmationDialogComponent,

    SectionComponent,
    RailwayPathComponent,
    TrainListComponent,
    TrainDialogComponent,
    SpeedDialogComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: FadeComponent },
      { path: 'fetch-data', component: FetchDataComponent },
    ]),
    BrowserAnimationsModule,
    MatCardModule, MatDialogModule, MatTooltipModule
  ],
  providers: [SignalRService],
  bootstrap: [AppComponent]
})
export class AppModule { }
