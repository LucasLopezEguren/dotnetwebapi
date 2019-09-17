import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { HttpRquestReportsService } from '../httpRequests/http-rquest-reports.service'
import { UserLogModelService } from '../models/user-log-model.service';
import { HttpRequest } from '@angular/common/http';


@Component({
  selector: 'app-top10-users',
  templateUrl: './top10-users.component.html',
  styleUrls: ['./top10-users.component.css']
})
export class Top10UsersComponent implements OnInit {
  @Output() logSelected = new EventEmitter<UserLogModelService>();
  logs: UserLogModelService[];
  selectedlog: UserLogModelService;

  constructor(private logService: HttpRquestReportsService) {
  }

  ngOnInit() {
    this.logService.getHttpReportUsuarios().subscribe(
      ((data: Array<UserLogModelService>) => this.result(data)),
      ((error: any) => console.log("Error", error))
    )
  }

  private result(data: Array<UserLogModelService>): void {
    this.logs = new Array<UserLogModelService>();
    let tempUsers = data;
    console.log("resultado reporte", data);
    tempUsers.forEach(log => {
      console.log("logs", log);
      this.logs.push(JSON.parse(JSON.stringify(log)));
    })
    this.logs.sort(function (a, b) {
      return a.ingresos- b.ingresos;
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
        if (log.usuario == clickedElement.usuario) {
          this.selectedlog = log;
        }
      })
      this.logSelected.next(this.selectedlog);
    }
  }
}
