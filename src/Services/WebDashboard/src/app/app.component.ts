import { Component, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  private _connection: HubConnection;

  constructor() {}

  ngOnInit(): void {
    this._connection = new HubConnectionBuilder().withUrl("http://localhost:5003/notification").build();

    this._connection
      .start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :('));

    this._connection.on('send', data => {
        console.log(data);
    })
  }

  title = 'WebDashboard';
}
