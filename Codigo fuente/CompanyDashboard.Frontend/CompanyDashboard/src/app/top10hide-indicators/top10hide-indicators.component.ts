import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { HttpRquestReportsService } from '../httpRequests/http-rquest-reports.service'
import { HttpRequest } from '@angular/common/http';
import { IndicatorsLogModelService } from '../models/indicators-log-model.service';


@Component({
  selector: 'app-top10hide-indicators',
  templateUrl: './top10hide-indicators.component.html',
  styleUrls: ['./top10hide-indicators.component.css']
})
export class Top10hideIndicatorsComponent implements OnInit {

  @Output() logSelected = new EventEmitter<IndicatorsLogModelService>();
  logs: IndicatorsLogModelService[];
  selectedlog: IndicatorsLogModelService;

  constructor(private logService: HttpRquestReportsService) {
  }

  ngOnInit() {
    this.logService.getHttpReportIndicadores().subscribe(
      ((data: Array<IndicatorsLogModelService>) => this.result(data)),
      ((error: any) => console.log("Error", error))
    )
  }

  private result(data: Array<IndicatorsLogModelService>): void {
    this.logs = new Array<IndicatorsLogModelService>();
    let tempUsers = data;
    console.log("resultado reporte", data);
    tempUsers.forEach(log => {
      console.log("logs", log);
      this.logs.push(JSON.parse(JSON.stringify(log)));
    })
    this.logs.sort(function (a, b) {
      return a.escondido- b.escondido;
    });
    this.logs.reverse();
  }

  onButtonGroupClick($event) {
    let clickedElement = $event.target || $event.srcElement;
    if (clickedElement.nodeName === "BUTTON") {
      let isButtonAlreadyActive = clickedElement.parentElement.parentElement.querySelector(".active");
      if (isButtonAlreadyActive) {
        isButtonAlreadyActive.classList.remove("active");
      }
      clickedElement.className += " active";
      console.log("clicked ", clickedElement);
      this.logs.forEach(log => {
        if (log.indicador == clickedElement.usuario) {
          this.selectedlog = log;
        }
      })
      this.logSelected.next(this.selectedlog);
    }
  }
}
