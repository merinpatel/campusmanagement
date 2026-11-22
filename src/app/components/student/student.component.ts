import { Component, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { StudentService } from '../../services/student.service';
import { Student } from './student.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrl: './student.component.css',
  imports:[ReactiveFormsModule,CommonModule]
})
export class StudentComponent implements OnInit {

  students: Student[] = [];
  filteredStudents=signal<Student[]>([]);

  form!: FormGroup;
  isEditMode = false;

  constructor(
    private fb: FormBuilder,
    private studentService: StudentService
  ) {}

  ngOnInit() {
    this.initForm();
    this.load();
  }

  /* ---------- FORM SETUP ---------- */

  initForm() {
    this.form = this.fb.group({
      studentId: [0],
      name: ['', Validators.required],
      rollNumber: ['', Validators.required],
      parentPhone: ['', Validators.required],
      pickupPoint: ['', Validators.required],
      isActive: [true]
    });
  }

  /* ---------- LOAD DATA ---------- */

  load() {
    this.studentService.getAll().subscribe(res => {
      this.students = res;
      this.filteredStudents.set(res);
    });
  }

  /* ---------- FILTERING ---------- */

  search(term: string) {
    term = term.toLowerCase();

    this.filteredStudents.set(this.students.filter(s =>
      s.name.toLowerCase().includes(term) ||
      s.rollNumber.toLowerCase().includes(term) ||
      s.pickupPoint.toLowerCase().includes(term)
     ));
  }

  filterActive(active: boolean) {
    this.filteredStudents.set( this.students.filter(x => x.isActive === active));
  }

  clearFilter() {
    this.filteredStudents.set( this.students);
  }

  /* ---------- CRUD ---------- */

  edit(student: Student) {
    this.isEditMode = true;
    this.form.patchValue(student);
  }

  resetForm() {
    this.isEditMode = false;
    this.form.reset({
      studentId: 0,
      isActive: true
    });
  }

  save() {
    if (this.form.invalid) return;

    const studentData = this.form.value as Student;

    if (this.isEditMode) {
      this.studentService.update(studentData)
        .subscribe(() => {
          this.load();
          this.resetForm();
        });
    } else {
      delete studentData.studentId;

      this.studentService.create(studentData)
        .subscribe(() => {
          this.load();
          this.resetForm();
        });
    }
  }

  delete(id: number) {
    if (!confirm('Delete this student?')) return;

    this.studentService.delete(id)
      .subscribe(() => this.load());
  }
}
