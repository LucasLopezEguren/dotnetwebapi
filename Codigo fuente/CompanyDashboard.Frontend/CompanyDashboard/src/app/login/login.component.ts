import { Component, OnInit } from '@angular/core';
import { UsersService } from '../models/users.service';
import { AreasService } from '../models/areas.service';
import { IndicatorsService } from '../models/indicators.service';
import { Router } from '@angular/router';
import { HttpLoginService } from '../httpRequests/http-login.service';
import { HttpRquestUsersService } from '../httpRequests/http-rquest-users.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  hide = true;


  user: UsersService = new UsersService("Lucas", "Lucaslopez", new Array<IndicatorsService>(), false);
  admin: UsersService = new UsersService("Admin", "Admin", new Array<IndicatorsService>(), true);
  users: UsersService[] = [this.user, this.admin]

  username: string;
  password: string;
  loggedUser: UsersService;
  isUserLogged: boolean = false;

  constructor(private router: Router, private loginService: HttpLoginService, private userService: HttpRquestUsersService) {

  }

  ngOnInit() {
  }

  login() {
    this.loginService.httpLogin(this.username, this.password).subscribe(
      ((data: string) => this.result(data)),
      ((error: any) => console.log("Error", error))
    );
    if (this.isUserLogged) {
      this.userService.getHttpUserByName(this.username).subscribe(
        ((data: UsersService) => this.getUser(data)),
        ((error: any) => console.log("Error", error))
      );
      this.redirect();
    } else {
      console.log("token: " + localStorage.getItem('token'));
    }
  }

  private result(data: string): void {
    console.log("Login data: ", data);
    localStorage.setItem('token', JSON.stringify(data));
    this.isUserLogged = true;
    this.redirect();
    this.userService.getHttpUserByName(this.username).subscribe(
      ((data: UsersService) => this.getUser(data)),
      ((error: any) => console.log("Error", error))
    );
    console.log("User logged " + this.isUserLogged);
  }

  private getUser(data: UsersService){
    let tmpUser = JSON.parse(JSON.stringify(data));
    console.log("user: ", tmpUser);
    this.user.admin = tmpUser.admin;
    this.user.id = tmpUser.id;
    this.user.lastname = tmpUser.lastname;
    this.user.listIndicator = new Array<IndicatorsService>();
    this.user.mail = tmpUser.mail;
    this.user.name = tmpUser.name;
    this.user.password = tmpUser.password;
    this.user.username = tmpUser.username;
    console.log("this user: ", this.user);
    localStorage.setItem('currentUser', JSON.stringify(this.user));
  }

  storeUsersInAreas() {
    this.user
  }

  redirect() {
    this.loggedUser = JSON.parse(localStorage.getItem('currentUser'));
    if (this.loggedUser.admin) {
      this.router.navigate(["admins"]);
    }
    else {
      this.router.navigate(["dashboard"]);
    }

  }

}
