import { Component, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { ToastService } from './services/toast.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'WebDashboard';
  private _connection: HubConnection;

  constructor(public toastService: ToastService) {}

  ngOnInit(): void {
    this._connection = new HubConnectionBuilder().withUrl("http://localhost:5003/notification").build();

    this._connection
      .start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :('));

    this._connection.on('send', data => {
      this.showCustomToast(data);
    })
  }

  showCustomToast(customTpl) {
    this.toastService.show(customTpl, {
      classname: 'bg-danger text-light',
      delay: 6000,
      autohide: true,
      headertext: 'Critical!'
    });
  }
}
