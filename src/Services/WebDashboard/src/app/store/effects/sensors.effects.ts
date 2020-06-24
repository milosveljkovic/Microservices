import { DataService } from 'src/app/services/data.service';
import { Injectable } from '@angular/core';
import { Actions, ofType, createEffect } from '@ngrx/effects';
import { map, mergeMap } from 'rxjs/operators';
import { SensorActionTypes, GetFilteredSensorsRequested } from '../actions/sensors.actions';

@Injectable()
export class SensorsEffects {

    constructor(private service: DataService, private actions: Actions) {}

    getSensors = createEffect(()=>
        this.actions
        .pipe(
            ofType(SensorActionTypes.GET_SENSORS_REQUESTED),
            mergeMap(()=> this.service.getSensors()
            .pipe(
                map(sensors => ({
                    type: SensorActionTypes.GET_SENSORS,
                    sensors: sensors
                }))
            ))
        )
    );

    getCriticalSensors = createEffect(()=>
        this.actions
        .pipe(
            ofType(SensorActionTypes.GET_CRITICAL_SENSORS_REQUESTED),
            mergeMap(()=> this.service.geCriticalData()
            .pipe(
                map(critical_sensors => ({
                    type: SensorActionTypes.GET_CRITICAL_SENSORS,
                    critical_sensors: critical_sensors
                }))
            ))
        )
    );

    filterData = createEffect(()=>
    this.actions
    .pipe(
        ofType<GetFilteredSensorsRequested>(SensorActionTypes.GET_FILTERED_SENSORS_REQUESTED),
        map((action)=> action.filter),
        mergeMap((filter)=> this.service.filterData(filter)
        .pipe(
            map(sensors => ({
                type: SensorActionTypes.GET_FILTERED_SENSORS,
                fsensors: sensors
            }))
        ))
    )
)

}