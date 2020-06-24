import { Action } from '@ngrx/store';
import { Sensor } from 'src/app/models/Sensor';

export enum SensorActionTypes {
    GET_SENSORS = 'GET_SENSORS',
    GET_SENSORS_REQUESTED = 'GET_SENSORS_REQUESTED',
    GET_CRITICAL_SENSORS = 'GET_CRITICAL_SENSORS',
    GET_CRITICAL_SENSORS_REQUESTED = 'GET_CRITICAL_SENSORS_REQUESTED',
    GET_FILTERED_SENSORS = 'GET_FILTERED_SENSORS',
    GET_FILTERED_SENSORS_REQUESTED = 'GET_FILTERED_SENSORS_REQUESTED'
  };

export class GetSensors implements Action {
    readonly type = SensorActionTypes.GET_SENSORS;
    constructor( public sensors: Sensor[]) {}
}

export class GetSensorsRequested implements Action {
    readonly type = SensorActionTypes.GET_SENSORS_REQUESTED;
    constructor() {}
}

export class GetCriticalSensors implements Action {
    readonly type = SensorActionTypes.GET_CRITICAL_SENSORS;
    constructor( public critical_sensors: Sensor[]) {}
}

export class GetCriticalSensorsRequested implements Action {
    readonly type = SensorActionTypes.GET_CRITICAL_SENSORS_REQUESTED;
    constructor() {}
}

export class GetFilteredSensors implements Action {
    readonly type = SensorActionTypes.GET_FILTERED_SENSORS;
    constructor( public fsensors: Sensor[]) {}
}

export class GetFilteredSensorsRequested implements Action {
    readonly type = SensorActionTypes.GET_FILTERED_SENSORS_REQUESTED;
    constructor( public filter: any) {}
}

export type SensorsActions = GetSensors 
                            | GetSensorsRequested 
                            | GetCriticalSensors 
                            | GetCriticalSensorsRequested
                            | GetFilteredSensors
                            | GetFilteredSensorsRequested; 
