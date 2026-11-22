import {
  AfterViewInit,
  Component,
  Inject,
  OnDestroy,
  OnInit,
  PLATFORM_ID,
  signal
} from '@angular/core';
import { DatePipe, isPlatformBrowser } from '@angular/common';
import { TrackingService } from '../../services/tracking.service';

type Leaflet = typeof import('leaflet');

interface LiveBus {
  busId: number;
  latitude: number;
  longitude: number;
  speed: number;
  recordedAt: string;
}

@Component({
  selector: 'app-tracking',
  templateUrl: './tracking.component.html',
  styleUrl: './tracking.component.css',
  imports: [DatePipe]
})
export class TrackingComponent implements OnInit, AfterViewInit, OnDestroy {
  buses = signal<LiveBus[]>([]);
  alerts = signal<any[]>([]);
  alertCount = signal<number>(0);

  private L?: Leaflet;
  private map?: import('leaflet').Map;
  private busIcon?: import('leaflet').Icon;
  private markers = new Map<number, import('leaflet').Marker>();
  private routeLines = new Map<number, import('leaflet').Polyline>();
  private routePoints = new Map<number, import('leaflet').LatLngExpression[]>();
  private readonly defaultCenter: import('leaflet').LatLngExpression = [23.0225, 72.5714];

  constructor(
    private trackingService: TrackingService,
    @Inject(PLATFORM_ID) private platformId: object
  ) {}

  ngOnInit(): void {
    this.trackingService.startConnection();

    this.trackingService.onBusLocation((data: any) => {
      const updatedBus = this.normalizeBus(data);
      this.updateBusList(updatedBus);
      this.updateBusOnMap(updatedBus);
    });

    this.trackingService.onAlert((alert) => {
      this.alerts.update((list) => [alert, ...list]);
      this.alertCount.update((count) => count + 1);
    });
  }

  async ngAfterViewInit(): Promise<void> {
    if (!isPlatformBrowser(this.platformId)) {
      return;
    }

    this.L = await import('leaflet');
    this.initializeMap();
  }

  ngOnDestroy(): void {
    this.trackingService.stopConnection();
    this.map?.remove();
  }

  trackByBusId(index: number, bus: LiveBus) {
    return bus.busId;
  }

  private initializeMap(): void {
    if (!this.L) {
      return;
    }

    this.busIcon = this.L.icon({
      iconUrl: 'assets/bus.png',
      iconSize: [44, 44],
      iconAnchor: [22, 22],
      popupAnchor: [0, -20]
    });

    this.map = this.L.map('liveBusMap', {
      center: this.defaultCenter,
      zoom: 14,
      zoomControl: true
    });

    this.L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution: '&copy; OpenStreetMap contributors'
    }).addTo(this.map);

    for (const bus of this.buses()) {
      this.updateBusOnMap(bus);
    }
  }

  private normalizeBus(data: any): LiveBus {
    return {
      busId: data.busId ?? data.BusId,
      latitude: data.latitude ?? data.Latitude,
      longitude: data.longitude ?? data.Longitude,
      speed: data.speed ?? data.Speed ?? 0,
      recordedAt: data.recordedAt ?? data.RecordedAt ?? new Date().toISOString()
    };
  }

  private updateBusList(updatedBus: LiveBus): void {
    this.buses.update((currentBuses) => {
      const index = currentBuses.findIndex((bus) => bus.busId === updatedBus.busId);

      if (index > -1) {
        const copy = [...currentBuses];
        copy[index] = updatedBus;
        return copy;
      }

      return [...currentBuses, updatedBus];
    });
  }

  private updateBusOnMap(bus: LiveBus): void {
    if (!this.L || !this.map || !this.busIcon) {
      return;
    }

    const position: import('leaflet').LatLngExpression = [bus.latitude, bus.longitude];
    const popupText = `Bus ${bus.busId} - ${Math.round(bus.speed || 0)} km/h`;
    const existingMarker = this.markers.get(bus.busId);

    if (existingMarker) {
      existingMarker.setLatLng(position).setPopupContent(popupText);
    } else {
      const marker = this.L.marker(position, { icon: this.busIcon })
        .addTo(this.map)
        .bindPopup(popupText);

      this.markers.set(bus.busId, marker);
      this.map.setView(position, 14);
    }

    this.updateRouteLine(bus.busId, position);
  }

  private updateRouteLine(busId: number, position: import('leaflet').LatLngExpression): void {
    if (!this.L || !this.map) {
      return;
    }

    const points = [...(this.routePoints.get(busId) ?? []), position].slice(-40);
    this.routePoints.set(busId, points);

    const existingLine = this.routeLines.get(busId);
    if (existingLine) {
      existingLine.setLatLngs(points);
      return;
    }

    const routeLine = this.L.polyline(points, {
      color: '#0f766e',
      weight: 4,
      opacity: 0.75
    }).addTo(this.map);

    this.routeLines.set(busId, routeLine);
  }
}
