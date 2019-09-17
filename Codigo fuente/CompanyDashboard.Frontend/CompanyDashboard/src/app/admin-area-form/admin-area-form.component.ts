import { Component, OnInit, Input, SimpleChanges } from '@angular/core';
import { AreasService } from '../models/areas.service';
import { HttpRequestAreasService } from '../httpRequests/http-request-areas.service';

@Component({
  selector: 'app-admin-area-form',
  templateUrl: './admin-area-form.component.html',
  styleUrls: ['./admin-area-form.component.css']
})
export class AdminAreaFormComponent implements OnInit {
  @Input() area: AreasService;
  name: string;
  datasource: string;

  constructor(private areaService: HttpRequestAreasService) {
    if(this.area != null){
      this.name = this.area.name;
      this.datasource = this.area.dataSource;
    } }

  ngOnInit() {
    if(this.area != null){
      this.name = this.area.name;
      this.datasource = this.area.dataSource;
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    if(this.area != null){
      this.name = this.area.name;
      this.datasource = this.area.dataSource;
    }
  }

  modify(){
    this.area.name = this.name;
    this.area.dataSource = this.datasource;
    this.areaService.putHttpAreas(this.area).subscribe(
      ((data: Array<AreasService>) => this.result(data)),
      ((error: any) => console.log("Error", error))
    )
  }

  register(){
    var newArea = new AreasService();
    newArea.name = this.name;
    newArea.dataSource = this.datasource
    this.areaService.postHttpAreas(newArea).subscribe(
      ((data: Array<AreasService>) => this.result(data)),
      ((error: any) => console.log("Error", error))
    )
   }

   private result(data: Array<AreasService>): void {
    let tempArea = data;
    console.log(JSON.parse(JSON.stringify(tempArea)));
  }
}
