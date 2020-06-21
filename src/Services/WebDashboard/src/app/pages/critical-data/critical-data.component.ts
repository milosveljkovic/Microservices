import { Component, OnInit } from '@angular/core';
import { CriticalDataService } from 'src/app/services/critical-data.service';
import { Sensor } from 'src/app/models/Sensor';
import { Observable } from 'rxjs';


@Component({
  selector: 'app-critical-data',
  templateUrl: './critical-data.component.html',
  styleUrls: ['./critical-data.component.css']
})
export class CriticalDataComponent implements OnInit {

  sensors: Observable<Sensor[]>;

  constructor(private criticalService : CriticalDataService) { }

  ngOnInit() {
    this.sensors = this.criticalService.geCriticalData();
  }

}
