import { Sensor } from 'src/app/models/Sensor';
import { SensorsActions, SensorActionTypes, GetCriticalSensors } from '../actions/sensors.actions';

export const initialState : Sensor[] = [];

export function criticalSensorsReducer(state: Sensor[] = initialState, action: SensorsActions) {
    switch(action.type){
        case SensorActionTypes.GET_CRITICAL_SENSORS: {
            const {critical_sensors} = action as GetCriticalSensors;
            return {...critical_sensors};
        }
        default:
            return state;
    }
}