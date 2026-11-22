import { Component, OnInit } from '@angular/core';
import { AlertService } from '../../services/alert.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-alerts',
  templateUrl: './alerts.component.html',
  imports:[DatePipe]
})
export class AlertsComponent implements OnInit {

  alerts: any[] = [];

  constructor(private alertService: AlertService) {}

  ngOnInit(): void {
    this.alertService.getAlerts().subscribe(
      res => this.alerts = res as any[]
    );
  }
}
