import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {ShowContactsComponent} from "./contacts/show-contacts/show-contacts.component";
import {AddContactsComponent} from "./contacts/add-contacts/add-contacts.component";
import {EditContactsComponent} from "./contacts/edit-contacts/edit-contacts.component";

const routes: Routes = [
  { path: 'Contacts/Show', component:ShowContactsComponent },
  { path: 'Contacts/Add', component:AddContactsComponent },
  { path: 'Contacts/Edit', component: EditContactsComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
