import { Component } from '@angular/core';
import { IndicatorsService } from '../app/models/indicators.service';
import { UsersService } from '../app/models/users.service';
import { AreasService } from './models/areas.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  constructor() {
  }
}
