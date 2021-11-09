import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppComponent} from './app.component';
import {SharedService} from "./shared.service";
import {HttpClientModule} from "@angular/common/http";
import {ContactsComponent} from "./contacts/contacts.component"
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {AppRoutingModule} from "./app-routing.module";
import {ShowContactsComponent} from "./contacts/show-contacts/show-contacts.component";
import {AddContactsComponent} from "./contacts/add-contacts/add-contacts.component";
import {EditContactsComponent} from "./contacts/edit-contacts/edit-contacts.component";

@NgModule({
  declarations: [
    AppComponent,
    ContactsComponent,
    ShowContactsComponent,
    AddContactsComponent,
    EditContactsComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    AppRoutingModule,
  ],
  providers: [SharedService],
  bootstrap: [AppComponent]
})
export class AppModule {
}
