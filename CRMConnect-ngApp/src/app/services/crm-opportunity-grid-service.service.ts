import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class CrmOpportunityGridServiceService {

  constructor(private http: HttpClient ) { }

  getAllOpportunityData(){
    const oppAPiUrl = 'http://localhost:5109/api/opportunity/getAllOpportunity'
    return this.http.get(oppAPiUrl);
  }
}
