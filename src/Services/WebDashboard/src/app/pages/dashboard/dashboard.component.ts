import { Component, OnInit } from '@angular/core';
import { Sensor } from 'src/app/models/Sensor';
import { DataService } from 'src/app/services/data.service';
import { Store } from '@ngrx/store';
import { GetSensorsRequested, GetFilteredSensorsRequested } from 'src/app/store/actions/sensors.actions';
import { State } from 'src/app/store/reducers/root.reducer';
import { NgbDateStruct, ModalDismissReasons, NgbModal, NgbDate, NgbDateParserFormatter, NgbCalendar } from '@ng-bootstrap/ng-bootstrap';
import { FormControl } from '@angular/forms';
import * as Highcharts from 'highcharts';
const moment=require('moment');


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  public options: any ={
    chart: {
        zoomType: 'x',
        type:'area'
    },
    title: {
        text: 'Representation of sensor data over time'
    },
    subtitle: {
        text: document.ontouchstart === undefined ?
            'Click and drag in the plot area to zoom in' : 'Pinch the chart to zoom in'
    },
    xAxis: {
        type: 'datetime',
        plotBands: [{ // visualize the weekend
          from: 4.5,
          to: 6.5,
          color: 'rgba(68, 170, 213, .2)'
      }]
    },
    tooltip:{
     pointFormat: '{series.name} : <b>{point.y:,.0f}</b><br/>'
    },
    yAxis: {
        title: {
            text: '(ug/m^3)'
        },
        plotLines: [{
          color: '#9DC8F1',
          value: '430', //PM10
          width: '1',
          zIndex: 2, 
          label:{
            text:"PM10 - 430",
            style: {
              color: '#9DC8F1',
              fontWeight: '100',
              fontSize: '10px'
          }
          }
          },{
          color: '#727276',
          value: '250', // PM25
          width: '1',
          zIndex: 2, 
          label:{
            text:"PM25 - 250",
            style: {
              color: '#727276',
              fontWeight: '100',
              fontSize: '10px'
          }
          }
          },{
          color: '#ACF29E',
          value: '1600', // SO2
          width: '1',
          zIndex: 2,
          label:{
            text:"SO2 - 1600",
            style: {
              color: '#ACF29E',
              fontWeight: '100',
              fontSize: '10px'
          }
          }
          },{
            color: '#E4B76D',
            value: '400', // NO2
            width: '1',
            zIndex: 2,
            label:{
            text:"NO2 - 400",
            style: {
              color: '#E4B76D',
              fontWeight: '100',
              fontSize: '10px'
          }
          }
          },{
            color: '#A0A4EF',
            value: '34000', // CO
            width: '1',
            zIndex: 2,
            label:{
            text:"CO - 34000",
            style: {
              color: '#A0A4EF',
              fontWeight: '100',
              fontSize: '10px'
          }
          }
          },{
            color: '#DD6E9C',
            value: '748', // O3
            width: '1',
            zIndex: 2,
            label:{
            text:"O3 - 748",
            style: {
              color: '#DD6E9C',
              fontWeight: '100',
              fontSize: '10px'
          }
          }
            }
      ]
    },
    legend: {
        enabled: true
    },
    plotOptions: {
      areaspline: {
          fillOpacity: 0.3
      }
  },

    series: []
}

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
      setTimeout(()=>{
        this.collectionSize = this.sensors.length;
      },300);
      if(this.sensors.length>0){
        let so2=[];let no2=[];let pm10=[];let pm25=[];let co=[];let o3=[];
        let i=0;
        for(i;i<this.sensors.length;i++){
          let so2_obj=[]; let pm25_obj=[];let pm10_obj=[];let no2_obj=[];let co_obj=[];let o3_obj=[];
          let currentDate=new Date(this.sensors[i].date);
          let date=moment(currentDate).valueOf();
          pm10_obj[0]=date;        pm10_obj[1]=this.sensors[i].pM10;
          pm25_obj[0]=date;        pm25_obj[1]=this.sensors[i].pM25;
          so2_obj[0]=date;         so2_obj[1]=this.sensors[i].sO2;
          no2_obj[0]=date;         no2_obj[1]=this.sensors[i].nO2;
          co_obj[0]=date;          co_obj[1]=this.sensors[i].co;
          o3_obj[0]=date;          o3_obj[1]=this.sensors[i].o3;
          pm25.push(pm25_obj);
          pm10.push(pm10_obj);
          so2.push(so2_obj);
          no2.push(no2_obj);
          co.push(co_obj);
          o3.push(o3_obj);
        }
        let chart=Highcharts.chart('container', this.options);
        chart.addSeries({
                  name:'PM10',
                  type:'area',
                  data:pm10
        })
        chart.addSeries({
                  name:'PM25',
                  type:'area',
                  data:pm25
          })
          chart.addSeries({
            name:'SO2',
            type:'area',
            data:so2
          })
          chart.addSeries({
            name:'NO2',
            type:'area',
            data:no2
          })
          chart.addSeries({
            name:'CO',
            type:'area',
            data:co
          })
          chart.addSeries({
            name:'O3',
            type:'area',
            data:o3
          })
      }
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
