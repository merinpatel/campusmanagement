import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { BusService } from '../../services/bus.service';
import { DriverService } from '../../services/driver.service';
import { StudentService } from '../../services/student.service';
import { AssignmentService } from '../../services/assignment.service';

@Component({
  selector: 'app-assignment',
  templateUrl: './assignment.component.html',
  styleUrl: './assignment.component.css',
  imports:[ReactiveFormsModule, CommonModule]
})
export class AssignmentComponent implements OnInit {

  buses: any[] = [];
  drivers: any[] = [];
  students: any[] = [];

  driverForm!: FormGroup;
  studentForm!: FormGroup;
  message = signal('');
  messageType = signal<'success' | 'error'>('success');

  constructor(
    private fb: FormBuilder,
    private busService: BusService,
    private driverService: DriverService,
    private studentService: StudentService,
    private assignmentService: AssignmentService
  ) {}

  ngOnInit() {
    this.loadData();
    this.initForms();
  }

  loadData() {
    this.busService.getAll().subscribe(res => this.buses = res);
    this.driverService.getAll().subscribe(res => this.drivers = res);
    this.studentService.getAll().subscribe(res => this.students = res);
  }

  initForms() {
    this.driverForm = this.fb.group({
      busId: [null, Validators.required],
      driverId: [null, Validators.required]
    });

    this.studentForm = this.fb.group({
      busId: [null, Validators.required],
      studentId: [null, Validators.required]
    });
  }

  assignDriver() {
    if (this.driverForm.invalid) return;

    const { busId, driverId } = this.driverForm.value;

    this.assignmentService.assignDriver(busId, driverId)
      .subscribe({
        next: () => {
          this.showMessage('Driver assigned successfully', 'success');
          this.driverForm.reset();
          this.loadData();
        },
        error: (err) => this.showMessage(this.getErrorMessage(err, 'Driver assignment failed'), 'error')
      });
  }

  assignStudent() {
    if (this.studentForm.invalid) return;

    const { busId, studentId } = this.studentForm.value;

    this.assignmentService.assignStudent(busId, studentId)
      .subscribe({
        next: () => {
          this.showMessage('Student assigned successfully', 'success');
          this.studentForm.reset();
          this.loadData();
        },
        error: (err) => this.showMessage(this.getErrorMessage(err, 'Student assignment failed'), 'error')
      });
  }

  removeDriver(driverId: number) {
    this.assignmentService.removeDriver(driverId).subscribe({
      next: () => {
        this.showMessage('Driver removed from bus', 'success');
        this.loadData();
      },
      error: (err) => this.showMessage(this.getErrorMessage(err, 'Unable to remove driver'), 'error')
    });
  }

  removeStudent(studentId: number) {
    this.assignmentService.removeStudent(studentId).subscribe({
      next: () => {
        this.showMessage('Student removed from bus', 'success');
        this.loadData();
      },
      error: (err) => this.showMessage(this.getErrorMessage(err, 'Unable to remove student'), 'error')
    });
  }

  busLabel(busId: number | null | undefined) {
    const bus = this.buses.find((item) => item.busId === busId);
    return bus ? bus.busNumber : 'Not assigned';
  }

  private showMessage(message: string, type: 'success' | 'error') {
    this.message.set(message);
    this.messageType.set(type);
  }

  private getErrorMessage(error: any, fallback: string) {
    return error?.error?.message || error?.error || fallback;
  }
}
