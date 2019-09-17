import { Component, OnInit } from '@angular/core';
import { UsersService } from '../models/users.service';

@Component({
  selector: 'app-admin-users',
  templateUrl: './admin-users.component.html',
  styleUrls: ['./admin-users.component.css']
})
export class AdminUsersComponent implements OnInit {
  user: UsersService;

  constructor() { }

  ngOnInit() {
  }

  setUser(userSelected) {
    this.user = userSelected;
  }

}
