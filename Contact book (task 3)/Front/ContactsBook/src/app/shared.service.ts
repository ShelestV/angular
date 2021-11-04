import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})

export class SharedService {
  readonly APIUrl="http://localhost:5000/api";
  readonly defaultId="00000000-0000-0000-0000-000000000000";

  constructor(private http:HttpClient) {
  }

  getContacts():Observable<any> {
    return this.http.get(this.APIUrl+'/Contact');
  }

  postContact(contact:any) {
    return this.http.post(this.APIUrl+'/Contact', contact);
  }

  putContact(contact: any) {
    return this.http.put(this.APIUrl+'/Contact', contact);
  }

  deleteContact(contactId: any) {
    return this.http.delete(this.APIUrl + '/Contact/' + contactId);
  }

  addPhone(phone: any) {
    return this.http.post(this.APIUrl + '/Phone', phone);
  }

  deletePhone(phoneNumber: any) {
    return this.http.delete(this.APIUrl + '/Phone/' + phoneNumber);
  }
}
