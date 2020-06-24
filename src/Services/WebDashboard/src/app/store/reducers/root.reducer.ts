import { Observable } from 'rxjs';

import { ActionReducerMap } from '@ngrx/store';
import { Settings } from 'src/app/models/Settings';
import { settingsReducer } from './settings.reducer';
import { Sensor } from 'src/app/models/Sensor';
import { sensorsReducer } from './sensors.reducer';
import { criticalSensorsReducer } from './critical-sensors.reducer';


export interface State {
    settings : Settings,
    sensors: Sensor[],
    critical_sensors: Sensor[]
}

export const rootReducer : ActionReducerMap<State> = {
    settings: settingsReducer,
    sensors: sensorsReducer,
    critical_sensors: criticalSensorsReducer
};