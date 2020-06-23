import { Injectable } from '@angular/core';
import { Actions, ofType, createEffect } from '@ngrx/effects';
import { map, mergeMap } from 'rxjs/operators';
import { SettingsService } from 'src/app/services/settings.service';
import { SettingsActionTypes } from '../actions/settings.action';


@Injectable()
export class SettingsEffect {

    constructor(private service: SettingsService, private actions: Actions) {}

    getSettings = createEffect(()=>
        this.actions
        .pipe(
            ofType(SettingsActionTypes.GET_SETTINGS_REQUESTED),
            mergeMap(()=> this.service.getSensorSettings()
            .pipe(
                map(s => ({
                    type: SettingsActionTypes.GET_SETTINGS,
                    settings: s
                }))
            ))
        )
    )

}