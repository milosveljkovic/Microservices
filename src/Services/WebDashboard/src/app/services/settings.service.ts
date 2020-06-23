import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Settings } from '../models/Settings';
import { Period } from '../models/Period';

const urlDevice = "http://localhost:8000/device/getSensorSettings";
const urlCommand = "/command"

const httpOptions = {
  headers: new HttpHeaders({
    'Access-Control-Allow-Origin':'*',
    'Content-Type':  'application/json',
    'Accept': "application/json, text/plain, */*"
  })
};

@Injectable({
  providedIn: 'root'
})

export class SettingsService {

  constructor(private http: HttpClient) { }

  public getSensorSettings(): Observable<Settings> {
    return this.http.get<Settings>(urlDevice);
  }

  public turnOnOffSensor(val : number): Observable<number> {
    return this.http.post<number>(urlCommand +'/turnOnOff',val, httpOptions);
  }

  public setReadPeriod(val: Period): Observable<Period> {
    return this.http.post<Period>(urlCommand + '/setSensorReadPeriod', val, httpOptions);
  }

  public setSendPeriod(val: Period): Observable<Period> {
    return this.http.post<Period>(urlCommand + '/setSensorSendPeriod', val, httpOptions);
  }

  public setTrashold(val: Number) : Observable<Number> {
    return this.http.post<Number>(urlCommand + '/setTreshold', val, httpOptions);
  }

  public turnOnOffXiaomi(val: number): Observable<number> {
    return this.http.post<number>(urlCommand + '/turnOnOffMiAirPurifier', val, httpOptions);
  }

  public setCleaningStrength(val: number): Observable<number> {
    return this.http.post<number>(urlCommand+ '/setMiAirPurfierCleaningStrength', val, httpOptions);
  }
}
