import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { IndicatorsService } from '../models/indicators.service';
import { HttpRquestUsersService } from '../httpRequests/http-rquest-users.service';
import { UsersService } from '../models/users.service';

@Component({
  selector: 'app-indicator-visibility',
  templateUrl: './indicator-visibility.component.html',
  styleUrls: ['./indicator-visibility.component.css']
})
export class IndicatorVisibilityComponent implements OnInit {
  @Output() changed = new EventEmitter<boolean>();
  @Input() indicator: IndicatorsService;
  isVisible: boolean;
  user: UsersService = JSON.parse(localStorage.getItem('currentUser'));

  constructor(private userService: HttpRquestUsersService) {
    this.user = JSON.parse(localStorage.getItem('currentUser'));
  }

  ngOnInit() {
    this.isVisible = this.indicator.visible;
    this.user
  }

  setVisibility() {
    this.indicator.visible = !this.indicator.visible
    this.changed.next(this.indicator.visible)
    this.userService.putHttpShowHideIndicator(this.user.id, this.indicator.id, !this.indicator.visible).subscribe(
      ((data: string) => this.result(data)),
      ((error: any) => console.log("Error", error))
    )
  }

  private result(data: string) {
    console.log("setVisibility ", data);
  }
}