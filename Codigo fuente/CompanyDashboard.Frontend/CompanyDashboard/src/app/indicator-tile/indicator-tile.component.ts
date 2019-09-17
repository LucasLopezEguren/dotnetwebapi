import { Component, OnInit, Input, ViewChild, ElementRef, SimpleChanges } from '@angular/core';
import { IndicatorsService } from '../models/indicators.service';
import { UsersService } from '../models/users.service';
import { HttpRquestUsersService } from '../httpRequests/http-rquest-users.service';
import { UserModelService } from '../models/user-model.service';

@Component({
  selector: 'app-indicator-tile',
  templateUrl: './indicator-tile.component.html',
  styleUrls: ['./indicator-tile.component.css']
})
export class IndicatorTileComponent implements OnInit {
  @Input() indicator: IndicatorsService;
  red: string;
  yellow: string;
  green: string;
  editable: boolean;

  constructor(private userService: HttpRquestUsersService) {
    this.editable = true;
  }

  @ViewChild("name", { static: false }) nameField: ElementRef;
  editName(): void {
    this.setEditable();
    this.nameField.nativeElement.focus();
  }

  saveChanges(event: any) {
    this.editable = true;
    this.indicator.name = event.target.value;
    let userid = JSON.parse(localStorage.getItem('currentUser')).id;
    this.userService.putHttpRename(userid, this.indicator.id, event.target.value).subscribe(
      ((data: string) => console.log("Rename ", data)),
      ((error: any) => console.log("Error ", error))
    );
  }

  setEditable() {
    this.editable = false;
  }

  setImages() {
    if (this.indicator.red) {
      console.log("el indicador quedo asi", this.indicator);
      this.red = "../../assets/Images/Red.png";
    } else {
      this.red = "../../assets/Images/Apagado.png";
    }
    if (this.indicator.green) {
      this.green = "../../assets/Images/Green.png";
    } else {
      this.green = "../../assets/Images/Apagado.png";
    }
    if (this.indicator.yellow) {
      this.yellow = "../../assets/Images/Yellow.png";
    } else {
      this.yellow = "../../assets/Images/Apagado.png";
    }
  }

  ngOnInit() {
    this.setImages();
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.setImages();
  }

}
