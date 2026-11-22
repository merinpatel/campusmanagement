import { Component, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { BusService } from '../../services/bus.service';
import { Bus } from './bus.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-bus',
  templateUrl: './bus.component.html',
  styleUrl: './bus.component.css',
  imports:[ReactiveFormsModule,CommonModule]
})
export class BusComponent implements OnInit {

  buses: Bus[] = [];
  filteredBuses=signal< Bus[]>([]); 

  form!: FormGroup;
  isEditMode = false;

  constructor(
    private fb: FormBuilder,
    private busService: BusService
  ) {}

  ngOnInit() {
    this.initForm();
    this.load();
  }

  initForm() {
    this.form = this.fb.group({
      busId: [0],
      busNumber: ['', Validators.required],
      capacity: [0, Validators.required],
      routeName: ['', Validators.required],
      isActive: [true]
    });
  }

  load() {
    this.busService.getAll().subscribe(res => {
      this.buses = res;
      this.filteredBuses.set(res);
    });
  }

  /* ---------- FILTER LOGIC ---------- */

  filterActive(active: boolean) {
    this.filteredBuses.set( this.buses.filter(x => x.isActive === active));
  }

  clearFilter() {
    this.filteredBuses .set( this.buses);
  }

  search(term: string) {
    term = term.toLowerCase();

    this.filteredBuses.set( this.buses.filter(bus =>
      bus.busNumber.toLowerCase().includes(term) ||
      bus.routeName.toLowerCase().includes(term)
    ));
  }

  /* ---------- CRUD LOGIC ---------- */

  edit(bus: Bus) {
    this.isEditMode = true;
    this.form.patchValue(bus);
  }

  resetForm() {
    this.isEditMode = false;
    this.form.reset({
      busId: 0,
      isActive: true,
      capacity: 0
    });
  }

  save() {
    if (this.form.invalid) return;

    const busData = this.form.value as Bus;

    if (this.isEditMode) {
      this.busService.update(busData)
        .subscribe(() => {
          this.load();
          this.resetForm();
        });
    } else {
      delete busData.busId;

      this.busService.create(busData)
        .subscribe(() => {
          this.load();
          this.resetForm();
        });
    }
  }

  delete(id: number) {
    if (!confirm('Delete this bus?')) return;

    this.busService.delete(id)
      .subscribe(() => this.load());
  }
}
