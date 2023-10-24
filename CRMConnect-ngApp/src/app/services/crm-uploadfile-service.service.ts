import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CrmUploadfileServiceService {
  private apiUrl = 'http://localhost:5109/api/contacts/uploadFileContacts';

  constructor(private http: HttpClient) {}

  uploadFile(formData: FormData) {
    return this.http.post(`${this.apiUrl}`, formData);
  }
}
