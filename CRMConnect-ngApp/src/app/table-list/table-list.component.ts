import { Component, OnInit, ChangeDetectorRef, Inject } from '@angular/core';
import { CrmDealsGridServiceService } from 'app/services/crm-deals-grid-service.service';
import { CrmOpportunityGridServiceService } from 'app/services/crm-opportunity-grid-service.service';
import { OpportunityStage, DealStage } from '../../app/layouts/admin-layout/generic-enums';

@Component({
  selector: 'app-table-list',
  templateUrl: './table-list.component.html',
  styleUrls: ['./table-list.component.css']
})
export class TableListComponent implements OnInit {
  opportunitiesDataList: any = []
  delasDataList; any = [];
  constructor(private oppGridService : CrmOpportunityGridServiceService,
    private dealsGridService : CrmDealsGridServiceService,
    @Inject(ChangeDetectorRef) public cdRef: ChangeDetectorRef) { }

  ngOnInit() {
    this.PopulateOppGridData();
    this.PopulateDealsGridData();
  }
  PopulateOppGridData(){
    this.oppGridService.getAllOpportunityData().subscribe(
      (res) => {
        this.opportunitiesDataList = res;
        this.cdRef && this.cdRef.detectChanges();
      }, 
      (error)=>{
        console.log(error)
      }
    );   
  }

  PopulateDealsGridData(){
    this.dealsGridService.getAllDealsData().subscribe(
      (res) => {
        this.delasDataList = res;
        this.cdRef && this.cdRef.detectChanges();
      }, 
      (error)=>{
        console.log(error)
      }
    );   
  }

  
  getOppStageName(stage: OpportunityStage): string {
    //lead, qualified lead, opportunity, closed won, closed lost
    switch (stage) {
      case OpportunityStage.OpportunityStage1:
        return 'Lead';
      case OpportunityStage.OpportunityStage2:
        return 'Qualified Lead';
      case OpportunityStage.OpportunityStage3:
        return 'Opportunity';
      case OpportunityStage.OpportunityStage4:
        return 'Closed Won';
      case OpportunityStage.OpportunityStage5:
        return 'Closed Lost';
              
      default:
        return 'Unknown Stage';
    }
  }

  getDealStageName(stage: DealStage): string{
    //open, pending, closed won, closed lost
    switch (stage) {
      case DealStage.DealStage1:
        return 'Open';
      case DealStage.DealStage2:
        return 'Pending';
      case DealStage.DealStage3:
        return 'Closed Won';
      case DealStage.DealStage4:
        return 'Closed Lost';      
      default:
        return 'Unknown Stage';
    }
  }
  // Edit Opportunity
  editOpportunity(opportunity: any) {
    // Implement your edit logic here, e.g., open a modal for editing
  }

  // Delete Opportunity
  deleteOpportunity(opportunity: any) {
    // Implement your delete logic here, e.g., show a confirmation dialog
  }
}
