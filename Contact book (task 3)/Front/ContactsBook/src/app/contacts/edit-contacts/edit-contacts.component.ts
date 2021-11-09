import {Component, OnInit} from "@angular/core";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {SharedService} from "../../shared.service";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'edit-contacts-component',
  templateUrl: './edit-contacts.component.html',
  styleUrls: ['./edit-contacts.component.css']
})

export class EditContactsComponent implements OnInit {

  contact: any;
  editableContact: FormGroup = new FormGroup({
    "name": new FormControl("", [
      Validators.required,
      Validators.maxLength(20),
    ]),
    "surname": new FormControl("", [
      Validators.maxLength(20),
    ]),
    "patronymic": new FormControl("", [
      Validators.maxLength(30),
    ])
  });

  constructor(private service: SharedService, private route: ActivatedRoute, private router: Router) {
  }


  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.contact = params
    })

    this.editableContact.setValue({
      "name": this.contact.name,
      "surname": this.contact.surname,
      "patronymic": this.contact.patronymic
    });
  }

  async editContact() {
    this.service.putContact({
      "id": this.contact.id,
      "name": this.editableContact?.get("name")?.value,
      "surname": this.editableContact?.get("surname")?.value,
      "patronymic": this.editableContact?.get("patronymic")?.value,
      "phones": [],
    }).subscribe(() => {
      this.router.navigate(['../Show'], {relativeTo: this.route});
    });
  }
}
