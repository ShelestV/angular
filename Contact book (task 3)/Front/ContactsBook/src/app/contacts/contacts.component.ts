import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {SharedService} from "../shared.service";

@Component({
  selector: 'contacts-component',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.css']
})

export class ContactsComponent implements OnInit {
  isActiveAddContact:boolean=false;
  isActiveEditContact:boolean=false;
  contacts:any = [];

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
  })

  editableContactId:any;
  editableContact: FormGroup = new FormGroup({
    "name":new FormControl("", [
      Validators.required,
      Validators.maxLength(20),
    ]),
    "surname":new FormControl("", [
      Validators.maxLength(20),
    ]),
    "patronymic":new FormControl("", [
      Validators.maxLength(30),
    ])
  })

  newPhoneNumber:string="";
  contactForAddingPhoneNumber:any;

  constructor(private service:SharedService) {  }

  ngOnInit():void {
    this.loadContacts();
  }

  openAddingForm():void {
    this.isActiveAddContact=true;
  }

  addContact():void {
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
    }).subscribe((data) => {
      this.loadContacts();
      this.isActiveAddContact = false;
    }, (error) => {
      console.error(error);
    });
  }

  getFullName(contact: any):string {
    return `${contact.surname} ${contact.name} ${contact.patronymic}`;
  }

  cancelAddingContact() {
    this.isActiveAddContact = false;
  }

  openAddingPhoneNumberForm(contact: any) {
    this.contactForAddingPhoneNumber=contact;
  }

  isOpenAddingPhoneNumberForm(contact: any):boolean {
    return this.contactForAddingPhoneNumber != undefined &&
      this.contactForAddingPhoneNumber.id === contact.id;
  }

  openEditContactForm(contact: any) {
    this.editableContactId = contact.id;
    this.editableContact.controls["name"].setValue(contact.name);
    this.editableContact.controls["surname"].setValue(contact.surname);
    this.editableContact.controls["patronymic"].setValue(contact.patronymic);

    this.isActiveEditContact = true;
  }

  cancelEditingContact() {
    this.isActiveEditContact=false;
  }

  editContact() {
    this.service.putContact({
      "id": this.editableContactId,
      "name": this.editableContact.get("name")?.value,
      "surname": this.editableContact.get("surname")?.value,
      "patronymic": this.editableContact.get("patronymic")?.value,
      "phones": [
        {
          "number": this.editableContact.get("phoneNumber")?.value,
          "contactId": this.editableContactId
        }
      ]
    }).subscribe((data) => {
      this.loadContacts();
      this.isActiveEditContact = false;
    }, (error) => {
      console.error(error);
    });
  }

  addPhoneNumber(contact: any) {
    this.service.addPhone({
      "number": this.newPhoneNumber,
      "note": "",
      "contactId": contact.id
    }).subscribe(() => {
      this.loadContacts();
    });
    this.contactForAddingPhoneNumber=undefined;
  }

  deleteContact(contact: any) {
    this.service.deleteContact(contact.id).subscribe(() => {
      this.loadContacts();
    });
  }

  deletePhone(phone: any) {
    this.service.deletePhone(phone.number).subscribe(() => {
      this.loadContacts();
    });
  }

  private loadContacts() {
    this.service.getContacts().subscribe(data => {
      this.contacts=data;
    });
  }
}
