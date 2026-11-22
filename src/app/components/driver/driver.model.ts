export interface Driver {
  driverId?: number;
  name: string;
  phone: string;
  licenseNumber: string;
  assignedBusId?: number;
  assignedBus?: any;
  isActive: boolean;
}
