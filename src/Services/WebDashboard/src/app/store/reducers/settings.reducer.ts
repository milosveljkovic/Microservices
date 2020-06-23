import { SettingsActions, SettingsActionTypes, GetSettings } from '../actions/settings.action';
import { Settings } from 'src/app/models/Settings';

export const initialState : Settings = {
    sendPeriod: 0,
    readPeriod: 0,
    treshold: 0,
    isOnSensor: 0,
    isOnMiAirPurfier: 0,
    cleaningStrengthMiAirPurfier: 0
}

export function settingsReducer(state: Settings = initialState, action: SettingsActions) {
    switch(action.type){
        case SettingsActionTypes.GET_SETTINGS: {
            const {settings} = action as GetSettings;
            return {...settings};
        }
        default:
            return state;
    }
}