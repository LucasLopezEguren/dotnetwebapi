import { Component, OnInit, Output, EventEmitter, Input, SimpleChanges } from '@angular/core';
import { UsersService } from '../models/users.service';
import { HttpRquestUsersService } from '../httpRequests/http-rquest-users.service';
import { UserModelService } from '../models/user-model.service';

@Component({
  selector: 'app-admin-users-list',
  templateUrl: './admin-users-list.component.html',
  styleUrls: ['./admin-users-list.component.css']
})
export class AdminUsersListComponent implements OnInit {
  @Output() userSelected = new EventEmitter<UsersService>();
  @Input() users: UsersService[];
  selectedUser: UsersService;
  reload: boolean;

  constructor(private userService: HttpRquestUsersService) {
  }

  ngOnInit() {
    this.loadUsers();
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.loadUsers();
  }

  loadUsers() {
    this.userService.getHttpUsers().subscribe(
      ((data: Array<UsersService>) => this.result(data)),
      ((error: any) => console.log("Error", error))
    )
  }

  private result(data: Array<UsersService>): void {
    this.users = new Array<UsersService>();
    let tempUsers = data;
    tempUsers.forEach(user => {
      this.users.push(JSON.parse(JSON.stringify(user)));
    })
    localStorage.setItem('allUsers', JSON.stringify(this.users));
  }

  onButtonGroupClick($event) {
    let clickedElement = $event.target || $event.srcElement;
    if (clickedElement.nodeName === "BUTTON") {
      let isButtonAlreadyActive = clickedElement.parentElement.parentElement.querySelector(".active");
      if (isButtonAlreadyActive) {
        isButtonAlreadyActive.classList.remove("active");
      }
      clickedElement.className += " active";
      console.log("clicked ", clickedElement);
      this.users.forEach(user => {
        if (user.username == clickedElement.name) {
          this.selectedUser = user;
        }
      })
      this.userSelected.next(this.selectedUser);
    }
  }

  delete() {
    if (this.selectedUser.username != JSON.parse(localStorage.getItem('currentUser')).Username) {
      const index: number = this.users.indexOf(this.selectedUser);
      if (index !== -1) {
        this.users.splice(index, 1);
      }
    }
    console.log("user to delete " + this.selectedUser.id)
    this.userService.deleteHttpUsers(this.selectedUser).subscribe(
      ((data: Array<UsersService>) => this.result(data)),
      ((error: any) => console.log("Error", error))
    )
  }
}
