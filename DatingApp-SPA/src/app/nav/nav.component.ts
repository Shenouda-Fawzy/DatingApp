import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  loginModel: any = {};

  constructor(public auth: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  login() {
    this.auth.login(this.loginModel).subscribe(next => {
      this.alertify.success('Logged In Successfully');
    }, error => {
      this.alertify.error(error);
    });
  }

  loggedIn(): boolean {
    return this.auth.isLoggedIn();
  }

  logout(){
    localStorage.removeItem('token');
    this.alertify.message('logged out');
    console.log('logged out');
  }
}
