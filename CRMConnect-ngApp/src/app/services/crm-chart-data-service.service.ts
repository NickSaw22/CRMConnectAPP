import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class CrmChartDataServiceService {

  constructor(private http : HttpClient) { }

  getOppBarChartData(){
    const barDataApiUrl = 'http://localhost:5109/api/opportunity/getOpportunityStatusWise';
    return this.http.get(barDataApiUrl);
  }
}
