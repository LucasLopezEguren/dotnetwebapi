import { Component, OnInit } from '@angular/core';
import { AreasService } from '../models/areas.service';

@Component({
  selector: 'app-admin-area',
  templateUrl: './admin-area.component.html',
  styleUrls: ['./admin-area.component.css']
})
export class AdminAreaComponent implements OnInit {
  areas: AreasService[] = JSON.parse(localStorage.getItem("areas"));
  area: AreasService;

  constructor() { }

  ngOnInit() {
    console.log(this.areas);
  }

  setUser(areaSelected) {
    this.area = areaSelected;
  }

}
