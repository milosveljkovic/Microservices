import { Observable } from 'rxjs';

import { ActionReducerMap } from '@ngrx/store';
import { Settings } from 'src/app/models/Settings';
import { settingsReducer } from './settings.reducer';

export interface State {
    settings : Settings
}

export const rootReducer : ActionReducerMap<State> = {
    settings: settingsReducer
};