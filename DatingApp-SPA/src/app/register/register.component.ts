import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model: any = {};

  @Output() canelRegister = new EventEmitter();

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  register() {
    this.authService.register(this.model).subscribe(() => {
      console.log('registeration successfull');
    }, error => {
      console.log('Error in registeration component');
      console.log(error);
    });
    console.log(this.model);
  }

  cancel() {
    console.log('Cancelled');
    this.canelRegister.emit(false);
  }
}
