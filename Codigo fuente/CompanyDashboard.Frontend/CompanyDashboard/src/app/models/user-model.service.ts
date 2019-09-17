import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserModelService {
  name: string;
  lastname: string;
  username: string;
  password: string;
  mail:  string;

  constructor() {
  }
}
