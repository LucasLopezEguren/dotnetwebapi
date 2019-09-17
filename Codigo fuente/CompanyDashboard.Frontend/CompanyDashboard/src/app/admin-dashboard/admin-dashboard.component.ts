import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UsersService } from '../models/users.service';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
  user: UsersService;

  constructor(private router: Router) {
    let token = JSON.parse(localStorage.getItem('token'));
    this.user = JSON.parse(localStorage.getItem('currentUser'));
    console.log("logged user: ", this.user);
    if (token == null || this.user == null || !this.user.admin){
      alert("Es necesario iniciar sesion para entrar en este sitio");
      this.logout();
    } }

  ngOnInit() {
  }


  logout() {
    localStorage.clear();
    this.router.navigate(["login"]);
  }

}
