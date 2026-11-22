
import {  Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { TrackingComponent } from './components/tracking/tracking.component';
import { AlertsComponent } from './components/alerts/alerts.component';
import { BusComponent } from './components/bus/bus.component';
import { DriverComponent } from './components/driver/driver.component';
import { StudentComponent } from './components/student/student.component';
import { AssignmentComponent } from './components/assignment/assignment.component';
import { DriverTrackingComponent } from './components/DriverTracking/DriverTrackingComponent';

export const routes: Routes = [
  {
    path: '',
    children: [
      { path: 'tracking', component: TrackingComponent },
      { path: 'alerts', component: AlertsComponent },
      {path:'bus',component:BusComponent},
      {path:'driver',component:DriverComponent},
      {path:'student',component:StudentComponent},
      
      {path:'Assign',component:AssignmentComponent},
      {path:'DriverTracking',component:DriverTrackingComponent}
    ]
  }
];

