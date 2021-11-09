import {Component, OnInit} from "@angular/core";
import {SharedService} from "../../shared.service";

@Component({
  selector: 'show-contacts-component',
  templateUrl: './show-contacts.component.html',
  styleUrls: ['./show-contacts.component.css']
})

export class ShowContactsComponent implements OnInit {

  editIcon: any;
  deleteIcon: any;

  contacts: any = [];

  newPhoneNumber: string = "";
  contactForAddingPhoneNumber: any;

  constructor(private service: SharedService) {

  }

  ngOnInit(): void {
    this.loadContacts();
  }

  getFullName(contact: any) {
    return contact.name + ' ' + contact.surname + ' ' + contact.patronymic;
  }

  openAddingPhoneNumberForm(contact: any) {
    this.contactForAddingPhoneNumber = contact;
  }

  isOpenAddingPhoneNumberForm(contact: any): boolean {
    return this.contactForAddingPhoneNumber != undefined &&
      this.contactForAddingPhoneNumber.id === contact.id;
  }

  addPhoneNumber(contact: any) {
    this.service.addPhone({
      "number": this.newPhoneNumber,
      "note": "",
      "contactId": contact.id
    }).subscribe(() => {
      this.loadContacts();
    });
    this.contactForAddingPhoneNumber = undefined;
    this.newPhoneNumber = "";
  }

  deletePhone(phone: any) {
    this.service.deletePhone(phone.number).subscribe(() => {
      this.loadContacts();
    })
  }

  deleteContact(contact: any) {
    this.service.deleteContact(contact.id).subscribe(() => {
      this.loadContacts();
    })
  }

  loadContacts() {
    this.service.getContacts().subscribe(data => {
      this.contacts = data;
    });
  }
}
