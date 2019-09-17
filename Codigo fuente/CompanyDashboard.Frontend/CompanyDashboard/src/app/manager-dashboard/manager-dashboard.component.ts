import { Component, OnInit } from '@angular/core';
import { UsersService } from '../models/users.service';
import { Router } from '@angular/router';
import { HttpRquestUsersService } from '../httpRequests/http-rquest-users.service';
import { IndicatorsService } from '../models/indicators.service';
import { HttpRequestsIndicatorsService } from '../httpRequests/http-requests-indicators.service';

@Component({
  selector: 'app-manager-dashboard',
  templateUrl: './manager-dashboard.component.html',
  styleUrls: ['./manager-dashboard.component.css']
})
export class ManagerDashboardComponent implements OnInit {
  user: UsersService = JSON.parse(localStorage.getItem('currentUser'));
  indicators: Array<IndicatorsService>;
  tempIndicators: Array<IndicatorsService>;

  constructor(private router: Router, private userService: HttpRquestUsersService, private indicatorService: HttpRequestsIndicatorsService) {
    let token = localStorage.getItem('token');
    this.user = JSON.parse(localStorage.getItem('currentUser'));
    this.indicators = new Array<IndicatorsService>();
    this.tempIndicators = new Array<IndicatorsService>();
    this.user.listIndicator = this.indicators;
    console.log("se construye", this.user);
    if (token == null) {
      alert("Es necesario loguearse para entrar en este sitio");
      this.router.navigate(["login"]);
    }
    this.userService.getHttpUserIndicators(this.user).subscribe(
      ((data: Array<IndicatorsService>) => console.log(this.result(data))),
      ((error: any) => console.log("Error", error))
    );
  }

  result(data: IndicatorsService[]): void {
    this.indicators = new Array<IndicatorsService>();
    this.tempIndicators = new Array<IndicatorsService>();
    let tempIndicator = data;
    console.log("tmp indicator: ", tempIndicator);
    tempIndicator.forEach(indicator => {
      indicator = JSON.parse(JSON.stringify(indicator));
      this.getColors(indicator);
    })
    this.getVisibles();
    this.user.listIndicator = this.indicators;
    console.log("user con indicators: ", this.user);
  }

  getColors(indicator: IndicatorsService) {
    this.indicatorService.getHttpColors(indicator.id).subscribe(
      ((retorno: Array<string>) => this.colors(retorno, indicator)),
      ((error: any) => console.log("Error", error))
    );
  }

  colors(data: Array<string>, indicator: IndicatorsService) {
    data = JSON.parse(JSON.stringify(data));
    console.log("colores ", data);
    let indicatorToAdd = new IndicatorsService();
    indicatorToAdd.id = indicator.id;
    indicatorToAdd.name = indicator.name;
    indicatorToAdd.greenText = data[0];
    indicatorToAdd.green = data[1] == "True";
    indicatorToAdd.yellowText = data[2];
    indicatorToAdd.yellow = data[3] == "True";
    indicatorToAdd.redText = data[4];
    indicatorToAdd.red = data[5] == "True";
    indicatorToAdd.visible = false;
    console.log("added: ", indicatorToAdd);
    this.tempIndicators.push(indicatorToAdd);
  }

  getVisibles() {
    this.userService.getHttpUserVisibleIndicators(this.user).subscribe(
      ((visibles: Array<IndicatorsService>) => this.setVisibility(visibles)),
      ((error: any) => console.log("Error", error))
    );
  }

  setVisibility(visibles: Array<IndicatorsService>) {
    console.log("setVisibility of", JSON.parse(JSON.stringify(visibles)));
    this.tempIndicators.forEach(indicator => {
      visibles.forEach(visibles => {
        if (indicator.id == visibles.id) {
          indicator.visible = true;
        }
      })
      this.indicators.push(indicator);
    });
    this.user.listIndicator = this.indicators;
    console.log("this.indicators with visibility", this.indicators );
  }

  ngOnInit() {
    this.tempIndicators = new Array<IndicatorsService>();
    let token = localStorage.getItem('token');
    this.user = JSON.parse(localStorage.getItem('currentUser'));
    console.log("se construye", this.user);
    if (token == null) {
      alert("Es necesario loguearse para entrar en este sitio");
      this.router.navigate(["login"]);
    }
    this.userService.getHttpUserIndicators(this.user).subscribe(
      ((data: Array<IndicatorsService>) => console.log(this.result(data))),
      ((error: any) => console.log("Error", error))
    );
  }
}
