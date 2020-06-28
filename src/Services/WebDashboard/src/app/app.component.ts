import { Component, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { ToastService } from './services/toast.service';
import { Sensor } from './models/Sensor';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'WebDashboard';
  private _connection: HubConnection;
  sensor : Sensor;

  constructor(public toastService: ToastService) {}

  ngOnInit(): void {
    this._connection = new HubConnectionBuilder().withUrl("http://localhost:5003/notification").build();

    this._connection
      .start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :('));

    this._connection.on('send', data => {
      this.sensor = data;
      this.showCustomToast('PM2.5 - '+this.sensor.pM25+'(ug/m^3) , PM10 - '+this.sensor.pM10+'(ug/m^3)'
      + ', SO2 - '+this.sensor.sO2+'(ug/m^3) , NO2 - '+this.sensor.nO2+'(ug/m^3)'
      + ', CO - '+this.sensor.co+'(ug/m^3) , O3 - '+this.sensor.o3+'(ug/m^3)');
    })
  }

  showCustomToast(customTpl) {
    this.toastService.show(customTpl, {
      classname: 'bg-danger text-light',
      delay: 8000,
      autohide: true,
      headertext: 'Critical!'
    });
  }
}
