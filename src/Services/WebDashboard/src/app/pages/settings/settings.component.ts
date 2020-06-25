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
  readSpeed: number;
  writeSpeed: number;
  error = false;
  error_trashold = false;
  error_xiaomi = false;

  constructor(private settingService : SettingsService, private store: Store<State>) { 
    this.store.select('settings').subscribe(settings=>{
      this.settings = settings
      this.xiaomi_enabled=this.settings.isOnMiAirPurfier===1?true:false
      this.sensors_enabled=this.settings.isOnSensor===1?true:false
      this.readSpeed = this.settings.readPeriod;
      this.writeSpeed = this.settings.sendPeriod;
    });
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
    if(val > this.readSpeed){
      this.error = false;
      this.settingService.setSendPeriod(this.period).subscribe(x=>console.log(x));
    }
    else {
      this.error = true;
    }
  }

  setReadPeriod(val) {
    this.period = {
      periodValue: parseInt(val)
    }
    if(val < this.writeSpeed){
      this.error = false;
      this.settingService.setReadPeriod(this.period).subscribe(x=>console.log(x));
    }
    else {
      this.error = true;
    }
  }

  setTreshold(val){
    if ( val < 1 || val > 49) {
      this.error_trashold = true;
    }
    else {
      this.error_trashold = false;
      this.settingService.setTrashold(val).subscribe(x=>console.log(x));
    }
  }

  setXiaomiStrength(val) {
    if ( val < 10 || val > 50) {
      this.error_xiaomi = true;
    }
    else {
      this.error_xiaomi = false;
      this.settingService.setCleaningStrength(val).subscribe(x=>console.log(x));
    }
  }

}
