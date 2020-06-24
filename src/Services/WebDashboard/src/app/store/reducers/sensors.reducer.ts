import { Sensor } from 'src/app/models/Sensor';
import { SensorsActions, SensorActionTypes, GetSensors, GetCriticalSensors, GetFilteredSensors } from '../actions/sensors.actions';

export const initialState : Sensor[] = []

export function sensorsReducer(state: Sensor[] = initialState, action: SensorsActions) {
    switch(action.type){
        case SensorActionTypes.GET_SENSORS: {
            const {sensors} = action as GetSensors;
            return {...sensors};
        }
        case SensorActionTypes.GET_FILTERED_SENSORS: {
            const {fsensors} = action as GetFilteredSensors;
            return {...fsensors}
        }
        default:
            return state;
    }
}