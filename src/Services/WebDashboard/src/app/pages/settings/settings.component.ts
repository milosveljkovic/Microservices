import { Component, OnInit, Input } from '@angular/core';
import { SettingsService } from 'src/app/services/settings.service';
import { Settings } from 'src/app/models/Settings';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';
import { State } from 'src/app/store/reducers/root.reducer';
import { GetSettings, GetSettingsRequested } from 'src/app/store/actions/settings.action';
import { Period } from 'src/app/models/Period';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {
  
  sensors_enabled : boolean;
  xiaomi_enabled : boolean; 
  settings: Settings; 
  period : Period;

  constructor(private settingService : SettingsService, private store: Store<State>) { 
    this.store.select('settings').subscribe(x=>this.settings = x);
  }

  ngOnInit() {
    this.store.dispatch(new GetSettingsRequested());
  }

  turnOnOffSensor(val) {
    this.settingService.turnOnOffSensor(val).subscribe(x=>console.log(x));;
  }

  turnOnOffXiaomi(val) {
    this.settingService.turnOnOffXiaomi(val).subscribe(x=>console.log(x));;
  }

  setSendPeriod(val) {
    this.period = {
      periodValue: parseInt(val)
    }
    this.settingService.setSendPeriod(this.period).subscribe(x=>console.log(x));
  }

  setReadPeriod(val) {
    this.period = {
      periodValue: parseInt(val)
    }
    this.settingService.setReadPeriod(this.period).subscribe(x=>console.log(x));
  }

  setTreshold(val){
    this.settingService.setTrashold(val).subscribe(x=>console.log(x));
  }

  setXiaomiStrength(val) {
    this.settingService.setCleaningStrength(val).subscribe(x=>console.log(x));
  }

}
