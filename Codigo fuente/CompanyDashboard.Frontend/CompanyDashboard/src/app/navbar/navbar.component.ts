import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { UsersService } from '../models/users.service';
import { IndicatorsService } from '../models/indicators.service';
import { HttpRquestUsersService } from '../httpRequests/http-rquest-users.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  @Output() changed = new EventEmitter<boolean>();
  @Input() user: UsersService;
  panelOpenState = false;
  order: number[];

  drop(event: CdkDragDrop<IndicatorsService[]>) {
    this.order = [];
    moveItemInArray(this.user.listIndicator, event.previousIndex, event.currentIndex);
    let position = 0;
    this.user.listIndicator.forEach(indicator => {
      console.log("indicador a ordenar ", indicator);
      position++;
      if (indicator.visible) {
        indicator.order = position;
        this.order.push(indicator.id);
      }
    })
    console.log("orden ", this.order);
    this.userService.putHttpReorder(this.user, this.order).subscribe(
      ((data: string) => this.result(data)),
      ((error: any) => console.log("Error", error))
    )
  }

  result(data: string) {
    console.log("nuevo orden: ", data)
  }

  constructor(private userService: HttpRquestUsersService) { }

  reCount() {
    this.changed.next(true);
  }

  ngOnInit() {
  }

}
