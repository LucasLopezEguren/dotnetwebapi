import { Injectable } from '@angular/core';
import { IndicatorsService } from './indicators.service';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  id: number;
  name: string;
  lastname: string;
  username: string;
  password: string;
  mail:  string;
  listIndicator: IndicatorsService[];
  admin: boolean;

  constructor(name: string, username: string, listIndicator: IndicatorsService[], isAdmin:boolean) {
    this.name = name;
    this.username = username;
    this.listIndicator = listIndicator
    this.admin = isAdmin;
  }
}
