import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SessionModelService {
  username: string;
  password: string;

  constructor(username:string,password:string) {
    this.password = password;
    this.username = username;
   }
}
