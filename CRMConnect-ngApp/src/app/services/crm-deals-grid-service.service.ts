import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class CrmDealsGridServiceService {

  constructor(private http : HttpClient) { }

  getAllDealsData(){
    const dealsApuUrl = 'http://localhost:5109/api/Deals/getAllDeals';
    return this.http.get(dealsApuUrl);
  }
}
