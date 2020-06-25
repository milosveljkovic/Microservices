import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { HomeComponent } from './pages/home/home.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { SettingsComponent } from './pages/settings/settings.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastComponent } from './components/toast/toast.component';
import { CriticalDataComponent } from './pages/critical-data/critical-data.component';
import { MomentModule } from 'ngx-moment';
import { StoreModule } from '@ngrx/store';
import { rootReducer } from './store/reducers/root.reducer';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { EffectsModule } from '@ngrx/effects';
import { SettingsEffect } from './store/effects/settings.effects';
import { SensorsEffects } from './store/effects/sensors.effects';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    DashboardComponent,
    SettingsComponent,
    ToastComponent,
    CriticalDataComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule,
    MomentModule,
    StoreModule.forRoot(rootReducer),
    EffectsModule.forRoot([SettingsEffect, SensorsEffects]),
    StoreDevtoolsModule.instrument({
      maxAge: 25
    }),
    ReactiveFormsModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
