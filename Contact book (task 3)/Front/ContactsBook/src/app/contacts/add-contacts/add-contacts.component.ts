import {Component, OnInit} from "@angular/core";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {SharedService} from "../../shared.service";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'add-contacts-component',
  templateUrl: './add-contacts.component.html',
  styleUrls: ['./add-contacts.component.css']
})

export class AddContactsComponent implements OnInit {
  newContact:FormGroup=new FormGroup({
    "name":new FormControl("", [
      Validators.required,
      Validators.maxLength(20),
    ]),
    "surname":new FormControl("", [
      Validators.maxLength(20),
    ]),
    "patronymic":new FormControl("", [
      Validators.maxLength(30),
    ]),
    "phoneNumber":new FormControl("", [
      Validators.required,
      Validators.pattern("^[0-9]{10}$")
    ])
  });

  constructor(private service: SharedService, private route: ActivatedRoute, private router: Router) {
  }

  ngOnInit(): void {
  }

  async addContact() {
    this.service.postContact({
      "id": this.service.defaultId,
      "name": this.newContact.get("name")?.value,
      "surname": this.newContact.get("surname")?.value,
      "patronymic": this.newContact.get("patronymic")?.value,
      "phones": [
        {
          "number": this.newContact.get("phoneNumber")?.value,
          "contactId": this.service.defaultId
        }
      ]
    }).subscribe(() => {
      this.router.navigate(['../Show'], { relativeTo: this.route })
    });
  }
}
