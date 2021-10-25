import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';

  isActiveForm=false;
  user={
    firstName:'',
    secondName:'',
    login:'',
    email:'',
    phoneNumber:'',
    birthDate:'',
    password:'',
    repeatPassword:'',
  }

  openReactiveForm() {

  }

  openForm() {
    this.isActiveForm = true;
  }

  signUp() {

  }
}
