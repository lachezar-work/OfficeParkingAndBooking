import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CalendarModule } from 'primeng/calendar';
import { DropdownModule } from 'primeng/dropdown';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { ToolbarModule } from 'primeng/toolbar';
import { TagModule } from 'primeng/tag';
import { MultiSelectModule } from 'primeng/multiselect';
import { ProgressBarModule } from 'primeng/progressbar';
import { CommonModule } from '@angular/common';
import { DatePipe } from '@angular/common';


import { AppComponent } from './app.component';
import { PresenceFormComponent } from './components/presence-form/presence-form.component';
import { CarManagementComponent } from './components/car-management/car-management.component';
import { SignInComponent } from './components/user/sign-in/sign-in.component';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    PresenceFormComponent,
    CarManagementComponent,
    SignInComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: '/presence', pathMatch: 'full' },
      { path: 'sign-in', component: SignInComponent },
      { path: 'presence', component: PresenceFormComponent },
      { path: 'cars', component: CarManagementComponent }
    ]),
    CalendarModule,
    DropdownModule,
    CheckboxModule,
    InputTextModule,
    InputTextareaModule,
    ButtonModule,
    TableModule,
    ToolbarModule,
    TagModule,
    MultiSelectModule,
    ProgressBarModule,
    CommonModule
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
