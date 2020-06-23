import { Action } from '@ngrx/store';
import { Settings } from 'src/app/models/Settings';

export enum SettingsActionTypes {
    GET_SETTINGS = 'GET_SETTINGS',
    GET_SETTINGS_REQUESTED = 'GET_SETTINGS_REQUESTED'
  };

export class GetSettings implements Action {
    readonly type = SettingsActionTypes.GET_SETTINGS;
    constructor( public settings: Settings) {}
}

export class GetSettingsRequested implements Action {
    readonly type = SettingsActionTypes.GET_SETTINGS_REQUESTED;
    constructor() {}
}

export type SettingsActions = GetSettings | GetSettingsRequested; 
