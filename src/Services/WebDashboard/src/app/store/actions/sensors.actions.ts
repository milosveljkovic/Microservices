import { Action } from '@ngrx/store';
import { Sensor } from 'src/app/models/Sensor';

export enum SensorActionTypes {
    GET_SENSOR = 'GET_SENSOR',
    GET_SENSOR_REQUESTED = 'GET_SENSOR_REQUESTED'
  };

export class GetSensors implements Action {
    readonly type = SensorActionTypes.GET_SENSOR;
    constructor( public sensors: Sensor[]) {}
}

export class GetSensorsRequested implements Action {
    readonly type = SensorActionTypes.GET_SENSOR_REQUESTED;
    constructor() {}
}

export type SensorsActions = GetSensors | GetSensorsRequested; 
