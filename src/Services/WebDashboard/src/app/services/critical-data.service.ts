import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Sensor } from '../models/Sensor';

const url = "http://localhost:5003/api/Sensor";

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json',
    'Authorization': 'my-auth-token'
  })
};

@Injectable({
  providedIn: 'root'
})

export class CriticalDataService {

  constructor(private http: HttpClient) { }

  public geCriticalData(): Observable<Sensor[]> {
    return this.http.get<Sensor[]>(url);
  }
}
