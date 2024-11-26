import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormBuilder } from '@angular/forms';
import { of } from 'rxjs';
import { PresenceFormComponent } from './presence-form.component';
import { DataService } from '../../services/data.service';
import { UserService } from '../user/user.service';

describe('PresenceFormComponent', () => {
  let component: PresenceFormComponent;
  let fixture: ComponentFixture<PresenceFormComponent>;
  let dataServiceMock: any;
  let userServiceMock: any;

  beforeEach(async () => {
    dataServiceMock = {
      getEmployees: jasmine.createSpy('getEmployees').and.returnValue(of([{ id: 1, name: 'John Doe' }])),
      getPresences: jasmine.createSpy('getPresences').and.returnValue(of([])),
      isParkingSpotAvailable: jasmine.createSpy('isParkingSpotAvailable').and.returnValue(true),
      addPresence: jasmine.createSpy('addPresence')
    };

    userServiceMock = {
      getUser: jasmine.createSpy('getUser').and.returnValue(of('john.doe@example.com'))
    };

    await TestBed.configureTestingModule({
      declarations: [PresenceFormComponent],
      providers: [
        FormBuilder,
        { provide: DataService, useValue: dataServiceMock },
        { provide: UserService, useValue: userServiceMock }
      ]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PresenceFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('getEmployeeName', () => {
    it('should return employee name when employeeId exists', () => {
      const employeeName = component.getEmployeeName(1);
      expect(employeeName).toBe('John Doe');
    });

    it('should return "Unknown" when employeeId does not exist', () => {
      const employeeName = component.getEmployeeName(2);
      expect(employeeName).toBe('Unknown');
    });

    it('should handle undefined employees', () => {
      dataServiceMock.getEmployees.and.returnValue(of(undefined));
      const employeeName = component.getEmployeeName(1);
      expect(employeeName).toBe('Unknown');
    });
  });
});