import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { UsersService } from '../models/users.service';
import { IndicatorsService } from '../models/indicators.service';

@Component({
  selector: 'app-admin-navbar',
  templateUrl: './admin-navbar.component.html',
  styleUrls: ['./admin-navbar.component.css']
})
export class AdminNavbarComponent implements OnInit {
  @Output() changed = new EventEmitter<boolean>();
  @Input() user: UsersService;
  panelOpenState = false;
  order:number[];

  drop(event: CdkDragDrop<IndicatorsService[]>) {
    this.order = [];
    moveItemInArray(this.user.listIndicator, event.previousIndex, event.currentIndex);
    let position = 0;
    this.user.listIndicator.forEach(indicator => {
      position++;
      if(indicator.visible) {
        indicator.order = position;
        this.order.push(indicator.id);
      }
    })
  }

  constructor() { }

  reCount() {
    this.changed.next(true);
  }

  ngOnInit() {
  }

}
