import {AbstractControl, FormControl, FormGroup, ValidatorFn, Validators} from '@angular/forms';
import {Component} from "@angular/core";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  title = 'app';

  maxDate;

  isActiveForm = false;
  isActiveReactiveForm = false;
  isSuccessfulRegistration = false;

  repeatPassword='';
  password = '';

  user = {
    firstName: '',
    secondName: '',
    login: '',
    email: '',
    phoneNumber: '',
    birthDate: '',
    password: '',
  }

  reactiveUser = new FormGroup({
    "firstName": new FormControl("", [
      Validators.required,
    ]),
    "secondName": new FormControl("", [
      Validators.required,
    ]),
    "login": new FormControl("", [
      Validators.required,
      Validators.pattern("[a-zA-Z0-9]*"),
    ]),
    "email": new FormControl("", [
      Validators.required,
      Validators.email,
    ]),
    "phoneNumber": new FormControl("", [
      Validators.required,
      Validators.pattern("^[0-9]{10}$"),
    ]),
    "birthDate": new FormControl("", [
      Validators.required,
      this.isCorrectDate(),
    ]),
    "password": new FormControl("", [
      Validators.required,
      Validators.pattern("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{6,}$"),
    ]),
    "repeatPassword": new FormControl("", [
      Validators.required,
      this.confirmPasswords(),
    ])
  });

  constructor() {
    let now = new Date();

    let maxYear = now.getFullYear() - 16;
    let maxMonth = now.getMonth();
    let maxDay = now.getDate();

    this.maxDate = new Date(maxYear, maxMonth, maxDay);
  }

  openReactiveForm() {
    this.isActiveReactiveForm = true;
  }

  openForm() {
    this.isActiveForm = true;
  }

  signUp() {
    if (this.isActiveForm) {
      console.log(JSON.stringify(this.user));
    }
    if (this.isActiveReactiveForm) {
      console.log(JSON.stringify({
        firstName: this.reactiveUser.get('firstName')?.value,
        secondName: this.reactiveUser.get('secondName')?.value,
        login: this.reactiveUser.get('login')?.value,
        email: this.reactiveUser.get('email')?.value,
        phoneNumber: this.reactiveUser.get('phoneNumber')?.value,
        birthDate: this.reactiveUser.get('birthDate')?.value,
        password: this.reactiveUser.get('password')?.value,
      }));
    }

    this.isSuccessfulRegistration = true;
    this.isActiveForm = false;
    this.isActiveReactiveForm = false;
  }

  private isCorrectDate(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: boolean } | null => {
      let valid = new Date(control.value) < this.maxDate;
      return valid ? null : {date: true};
    };
  }

  private confirmPasswords(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: boolean } | null => {
      let valid = this.password === control.value;
      return valid ? null : {confirm: true};
    };
  }
}
