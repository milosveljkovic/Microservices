import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Sensor } from '../models/Sensor';

const urlAnalytics = "http://localhost:8000/analytics/sensor";
const urlData = "/dataservice";

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json',
    'Authorization': 'my-auth-token'
  })
};

@Injectable({
  providedIn: 'root'
})

export class DataService {

  constructor(private http: HttpClient) { }

  public geCriticalData(): Observable<Sensor[]> {
    return this.http.get<Sensor[]>(urlAnalytics);
  }

  public getSensors() : Observable<Sensor[]> {
    return this.http.get<Sensor[]>(urlData + '/sensor');
  }

  public filterData(filter) : Observable<Sensor[]> {
    return this.http.post<Sensor[]>(urlData + '/getFilteredData', filter, httpOptions);
  }

}
