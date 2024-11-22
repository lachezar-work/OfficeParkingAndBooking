import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';

interface WeatherForecast {
    date: string;
    temperatureC: number;
    temperatureF: number;
    summary: string; 
}

@Component({
    selector: 'app-weather-forecast',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './weather-forecast.component.html',
    styleUrls: ['./weather-forecast.component.css']
})
export class WeatherForecastComponent {
    public forecasts: WeatherForecast[] = [];

    constructor(private http: HttpClient) {}

    ngOnInit() {
        this.getForecasts();
    }

    getForecasts() {
        this.http.get<WeatherForecast[]>('/weatherforecast').subscribe(
          {
            next: (result) => {
              this.forecasts = result;
            },
            error: (error) => {
              console.error(error);
            },
            complete: () => {
              console.log('Weather data loaded successfully');
            }
          }

        );
    }
  }
