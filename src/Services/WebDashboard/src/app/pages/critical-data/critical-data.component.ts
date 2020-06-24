import { Component, OnInit } from '@angular/core';
import { DataService } from 'src/app/services/data.service';
import { Sensor } from 'src/app/models/Sensor';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';
import { GetCriticalSensorsRequested } from 'src/app/store/actions/sensors.actions';
import { State } from 'src/app/store/reducers/root.reducer';


@Component({
  selector: 'app-critical-data',
  templateUrl: './critical-data.component.html',
  styleUrls: ['./critical-data.component.css']
})
export class CriticalDataComponent implements OnInit {

  sensors: Sensor[];

  constructor(private dataService : DataService, private store : Store<State>) {
    this.store.select('critical_sensors').subscribe( sensors => {
      this.sensors = Object.values(sensors);
   });
  }

  ngOnInit() {
    this.store.dispatch(new GetCriticalSensorsRequested());
  }

}
