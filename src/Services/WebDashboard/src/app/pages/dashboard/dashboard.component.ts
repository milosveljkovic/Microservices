import { Component, OnInit } from '@angular/core';
import { Sensor } from 'src/app/models/Sensor';
import { DataService } from 'src/app/services/data.service';
import { Store } from '@ngrx/store';
import { GetSensorsRequested, GetFilteredSensorsRequested } from 'src/app/store/actions/sensors.actions';
import { State } from 'src/app/store/reducers/root.reducer';
import { NgbDateStruct, ModalDismissReasons, NgbModal, NgbDate, NgbDateParserFormatter, NgbCalendar } from '@ng-bootstrap/ng-bootstrap';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  sensors: Sensor[];
  model: NgbDateStruct;
  closeResult = '';
  error = false;
  page = 1;
  pageSize = 10;
  collectionSize : number;

  dateFrom = new FormControl('');
  dateTo = new FormControl('');
  pm25 = new FormControl(0);
  pm10 = new FormControl(0);
  so2 = new FormControl(0);
  no2 = new FormControl(0);
  co = new FormControl(0);
  o3 = new FormControl(0);
  

  constructor(private dataService : DataService, private store : Store<State>,private modalService: NgbModal, public formatter: NgbDateParserFormatter) {
    this.store.select('sensors').subscribe( sensors => {
      this.sensors = Object.values(sensors);
      this.collectionSize = this.sensors.length;
    });
  }

  ngOnInit() {
    this.store.dispatch(new GetSensorsRequested());
  }

  get sensorss(): Sensor[] {
    return this.sensors
      .map((sensor, i) => ({id: i + 1, ...sensor}))
      .slice((this.page - 1) * this.pageSize, (this.page - 1) * this.pageSize + this.pageSize);
  }

  open(content) {
    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'}).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }

  onSubmit() {
    let date_from = this.dateFrom.value.year + '-' + this.dateFrom.value.month + '-' + this.dateFrom.value.day;
    let date_to = this.dateTo.value.year + '-' + this.dateTo.value.month + '-' + this.dateTo.value.day;

    if(date_from !== 'undefined-undefined-undefined' && date_to !== 'undefined-undefined-undefined'){
      let filter = {
        dateFrom: date_from,
        dateTo: date_to,
        pm25: this.pm25.value,
        pm10: this.pm10.value,
        so2: this.so2.value,
        no2: this.no2.value,
        co: this.co.value,
        o3: this.o3.value
      }
      this.error = false;
      this.store.dispatch(new GetFilteredSensorsRequested(filter));
    }
    else {
      this.error = true;
    }
  }

  onClear() {
    this.pm25.setValue(0);
    this.pm10.setValue(0);
    this.so2.setValue(0);
    this.no2.setValue(0);
    this.co.setValue(0);
    this.o3.setValue(0);
  }


}
