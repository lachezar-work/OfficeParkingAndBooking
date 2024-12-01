import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DataService } from '../../services/data.service';
import { map } from 'rxjs/operators';
import { Car, Employee } from '../../models/models';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-car-management',
  templateUrl: './car-management.component.html',
  styleUrls: ['./car-management.component.css']
})
export class CarManagementComponent {
  carForm: FormGroup;
  employees: Employee[] = [];
  cars: Car[] = [];
  displayedColumns = ['employeeName', 'brand', 'registrationPlate'];

  constructor(
    private fb: FormBuilder,
    private dataService: DataService
  ) {

    this.carForm = this.fb.group({
      employeeId: ['', Validators.required],
      brand: ['', Validators.required],
      registrationPlate: ['', Validators.required]
    });
  }
  ngOnInit(): void {
    this.dataService.getEmployees().subscribe((employees: Employee[]) => {
      this.employees = employees.map(employee => ({
        ...employee,
        fullNameWithTeam: `${employee.fullName} | ${employee.teamName}`}));
    });

    this.dataService.getCars().pipe(
      map(cars => 
        cars.map(car => ({
          ...car,
          employeeName: this.dataService.getEmployeeName(car.employeeId)
        }))
      )
    ).subscribe((cars: Car[]) => {
      this.cars = cars;
    });
  }

  onSubmit() {
    if (this.carForm.valid) {
      this.dataService.addCar({
        id: 0,
        employeeId: this.carForm.value.employeeId,
        brand: this.carForm.value.brand,
        registrationPlate: this.carForm.value.registrationPlate
      });
      this.carForm.reset();
    }
  }
}