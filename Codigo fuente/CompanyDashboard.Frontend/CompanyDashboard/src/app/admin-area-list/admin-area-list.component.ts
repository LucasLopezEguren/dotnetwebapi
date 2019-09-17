import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AreasService } from '../models/areas.service';
import { HttpRequestAreasService } from '../httpRequests/http-request-areas.service'

@Component({
  selector: 'app-admin-area-list',
  templateUrl: './admin-area-list.component.html',
  styleUrls: ['./admin-area-list.component.css']
})
export class AdminAreaListComponent implements OnInit {
  @Output() areaSelected = new EventEmitter<AreasService>();
  areas: AreasService[];
  selectedArea: AreasService;

  constructor(private areaService: HttpRequestAreasService) {
  }

  ngOnInit() {
    this.areaService.getHttpAreas().subscribe(
      ((data: Array<AreasService>) => this.result(data)),
      ((error: any) => console.log("Error", error))
    )
  }

  private result(data: Array<AreasService>): void {
    this.areas = new Array<AreasService>();
    let tempUsers = data;
    tempUsers.forEach(area => {
      this.areas.push(JSON.parse(JSON.stringify(area)));
    })
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
      this.areas.forEach(area => {
        if (area.name == clickedElement.name) {
          this.selectedArea = area;
        }
      })
      this.areaSelected.next(this.selectedArea);
    }
  }

  delete() {
    const index: number = this.areas.indexOf(this.selectedArea);
    if (index !== -1) {
      this.areas.splice(index, 1);
    }
    console.log("area to delete " + this.selectedArea.id)
    this.areaService.deleteHttpAreas(this.selectedArea).subscribe(
      ((data: AreasService) => this.deleted(data)),
      ((error: any) => console.log("Error", error))
    )
  }

  deleted(data: AreasService){
    console.log(JSON.stringify(data));
  }

}
