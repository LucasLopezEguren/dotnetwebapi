import { Component, OnInit, Input } from '@angular/core';
import { UsersService } from '../models/users.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  @Input() user: UsersService;
  title = 'CompanyDashboard';
  
  constructor() { }

  ngOnInit() {
  }

}
