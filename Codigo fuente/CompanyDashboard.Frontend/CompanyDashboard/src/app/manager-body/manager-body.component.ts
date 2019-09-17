import { Component, OnInit, Input, SimpleChanges } from '@angular/core';
import { IndicatorsService } from '../models/indicators.service';
import { UsersService } from '../models/users.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-manager-body',
  templateUrl: './manager-body.component.html',
  styleUrls: ['./manager-body.component.css']
})
export class ManagerBodyComponent implements OnInit {
  @Input() user: UsersService;

  @Input() indicators: IndicatorsService[];
  greenCount: number;
  yellowCount: number;
  redCount: number;
  visibleCount: number;


  constructor(private router: Router) {
    let token = JSON.parse(localStorage.getItem('token'));
    this.user = JSON.parse(localStorage.getItem('currentUser'));
    //this.indicators = this.user.listIndicator;
    console.log("token: ", token + "\n user: ", this.user);

    if (token == null || this.user == null) {
      this.logout();
    }
    if (this.user != null && this.user.admin) {
      this.router.navigate(["admins"]);
    }
  }

  ngOnInit() {
    this.greenCount = 0;
    this.yellowCount = 0;
    this.redCount = 0;
    this.visibleCount = 0;
    this.indicators = this.user.listIndicator;
    this.indicators.forEach(indicator => {
      if (indicator.green && indicator.visible) { this.greenCount++; }
      if (indicator.red && indicator.visible) { this.redCount++; }
      if (indicator.yellow && indicator.visible) { this.yellowCount++; }
      if (indicator.visible) { this.visibleCount++; }
    })
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.greenCount = 0;
    this.yellowCount = 0;
    this.redCount = 0;
    this.visibleCount = 0;
    //this.indicators = this.user.listIndicator;
    this.indicators.forEach(indicator => {
      if (indicator.green && indicator.visible) { this.greenCount++; }
      if (indicator.red && indicator.visible) { this.redCount++; }
      if (indicator.yellow && indicator.visible) { this.yellowCount++; }
      if (indicator.visible) { this.visibleCount++; }
    })
    this.reCount();
  }

  logout() {
    localStorage.clear();
    this.router.navigate(["login"]);
  }

  reCount() {
    this.greenCount = 0;
    this.yellowCount = 0;
    this.redCount = 0;
    this.visibleCount = 0;
    //this.indicators = this.user.listIndicator;
    this.indicators.forEach(indicator => {
      if (indicator.green && indicator.visible) { this.greenCount++; }
      if (indicator.red && indicator.visible) { this.redCount++; }
      if (indicator.yellow && indicator.visible) { this.yellowCount++; }
      if (indicator.visible) { this.visibleCount++; }
    })
  }

}
