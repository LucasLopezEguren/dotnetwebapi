import { Component, OnInit, Input, SimpleChanges } from '@angular/core';
import { UsersService } from '../models/users.service';
import { HttpRquestUsersService } from '../httpRequests/http-rquest-users.service';
import { UserModelService } from '../models/user-model.service';

@Component({
  selector: 'app-admin-users-form',
  templateUrl: './admin-users-form.component.html',
  styleUrls: ['./admin-users-form.component.css']
})
export class AdminUsersFormComponent implements OnInit {
  @Input() user: UsersService;
  reload: boolean = false;
  name: string;
  lastname: string;
  username: string;
  mail: string;
  password: string;
  hide: boolean;

  constructor(private userService: HttpRquestUsersService) {
    if (this.user == null) {
    } else {
      this.name = this.user.name;
      this.lastname = this.user.lastname;
      this.mail = this.user.mail;
      this.username = this.user.username;
      this.password = this.user.password;
    }
    this.reload = !JSON.parse(localStorage.getItem('reload'));
  }

  ngOnInit() {
    if (this.user == null) {

    } else {
      this.name = this.user.name;
      this.lastname = this.user.lastname;
      this.mail = this.user.mail;
      this.username = this.user.username;
      this.password = this.user.password;
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (this.user == null) {

    } else {
      this.name = this.user.name;
      this.lastname = this.user.lastname;
      this.mail = this.user.mail;
      this.username = this.user.username;
      this.password = this.user.password;
    }
  }

  modify() {
    this.user.name = this.name;
    this.user.lastname = this.lastname;
    this.user.mail = this.mail;
    this.user.username = this.username;
    this.user.password = this.password;
    this.userService.putHttpUsers(this.user).subscribe(
      ((data: Array<UsersService>) => this.result(data)),
      ((error: any) => console.log("Error", error))
    )
  }

  private result(data: Array<UsersService>): void {
    let tempUsers = data;
    console.log(JSON.parse(JSON.stringify(tempUsers)));
  }

  register() {
    let newUser: UserModelService = new UserModelService();
    newUser.lastname = this.lastname;
    newUser.username = this.username;
    newUser.name = this.name;
    newUser.password = this.password;
    newUser.mail = this.mail;
    this.userService.postHttpUsers(newUser).subscribe(
      ((data: Array<UsersService>) => this.result(data)),
      ((error: any) => console.log("Error", error))
    )
    localStorage.setItem('reload', JSON.stringify(this.reload));
   }
}
