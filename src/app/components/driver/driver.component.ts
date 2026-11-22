import { Component, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { DriverService } from '../../services/driver.service';
import { Driver } from './driver.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-driver',
  templateUrl: './driver.component.html',
  styleUrl: './driver.component.css',
  imports:[ReactiveFormsModule,CommonModule]
})
export class DriverComponent implements OnInit {

  drivers: Driver[] = [];
  filteredDrivers=signal<Driver[]>([]);

  form!: FormGroup;
  isEditMode = false;

  constructor(
    private fb: FormBuilder,
    private driverService: DriverService
  ) {}

  ngOnInit() {
    this.initForm();
    this.load();
  }

  initForm() {
    this.form = this.fb.group({
      driverId: [0],
      name: ['', Validators.required],
      phone: ['', Validators.required],
      licenseNumber: ['', Validators.required],
      isActive: [true]
    });
  }

  load() {
    this.driverService.getAll().subscribe(res => {
      this.drivers = res;
      this.filteredDrivers.set(res);
    });
  }

  search(term: string) {
    term = term.toLowerCase();

    this.filteredDrivers.set( this.drivers.filter(d =>
      d.name.toLowerCase().includes(term) ||
      d.phone.includes(term)
    ))
  }

  filterActive(active: boolean) {
    this.filteredDrivers.set(this.drivers.filter(x => x.isActive === active));
  }

  clearFilter() {
    this.filteredDrivers .set( this.drivers);
  }

  edit(driver: Driver) {
    this.isEditMode = true;
    this.form.patchValue(driver);
  }

  resetForm() {
    this.isEditMode = false;
    this.form.reset({
      driverId: 0,
      isActive: true
    });
  }

  save() {
    if (this.form.invalid) return;

    const driverData = this.form.value as Driver;

    if (this.isEditMode) {
      this.driverService.update(driverData)
        .subscribe(() => {
          this.load();
          this.resetForm();
        });
    } else {
      delete driverData.driverId;

      this.driverService.create(driverData)
        .subscribe(() => {
          this.load();
          this.resetForm();
        });
    }
  }

  delete(id: number) {
    if (!confirm('Delete this driver?')) return;

    this.driverService.delete(id)
      .subscribe(() => this.load());
  }
}
