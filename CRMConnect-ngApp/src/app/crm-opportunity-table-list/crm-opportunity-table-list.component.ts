import { Component, OnInit, ChangeDetectorRef, Inject, ViewChild, ElementRef } from '@angular/core';
import { CrmDealsGridServiceService } from 'app/services/crm-deals-grid-service.service';
import { CrmOpportunityGridServiceService } from 'app/services/crm-opportunity-grid-service.service';
import { OpportunityStage, DealStage } from '../../app/layouts/admin-layout/generic-enums';
import { AgGridAngular } from 'ag-grid-angular';
import { CellClickedEvent, CellValueChangedEvent, ColDef, GridReadyEvent, ICellRendererParams, NewValueParams } from 'ag-grid-community';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { CrmUploadfileServiceService } from 'app/services/crm-uploadfile-service.service';
import { CrmCustomGridDropdownEditorComponent } from 'app/crm-custom-grid-dropdown-editor/crm-custom-grid-dropdown-editor.component';
import { DatePipe } from '@angular/common';
import { CrmAddOpportunityRowSliderComponent } from 'app/crm-add-opportunity-row-slider/crm-add-opportunity-row-slider.component';

@Component({
  selector: 'app-crm-opportunity-table-list',
  templateUrl: './crm-opportunity-table-list.component.html',
  styleUrls: ['./crm-opportunity-table-list.component.css']
})
export class CrmOpportunityTableListComponent implements OnInit {
  gridApi: any;
  gridColumnApi: any;
  opportunitiesDataList: any = []
  delasDataList; any = [];
  rowsPerPage = 5;
  sliderOpen = false;
  public columnDefs: ColDef[] = [
    { field: 'name',  editable: false},
    { field: 'account.name',  editable: false},
    { field: 'contact',  editable: false,
      valueFormatter: (params) => {
        return params.value.firstName + ' ' + params.value.lastName; // Display stage name
      },
    },
    { 
      field: 'closingDate',  
      editable: false, 
      valueFormatter: (params) =>{
        return this.datePipe.transform(params.value, 'shortDate');
      }
    },
    { field: 'amount',  editable: true},
    // { field: 'emailAddress',  editable: true},
    {
      field: 'stage',
      editable: true,
      cellEditor: 'agSelectCellEditor',
      cellRenderer: this.getSelectedValue.bind(this),
      cellEditorParams: {
        values: [1, 2, 3, 4, 5], // Numeric values
      },
      valueFormatter: (params) => {
        return this.getOppStageName(params.value); // Display stage name
      },
      onCellValueChanged: this.handleCellValueChanged.bind(this),
      
    }
  ];

  // DefaultColDef sets props common to all Columns
  public defaultColDef: ColDef = {
    sortable: true,
    filter: true,   
    resizable: true
  };
  getSelectedValue(params: ICellRendererParams): string {
    const stage = this.getOppStageName(params.value)
    return stage;
    // return params.value ? 
    //   params.value :
    //   (['Failed', 'Completed'].includes(params.data.status)) && !params.value ? 'Selecte Stage' : '';
  }

  // Data that gets displayed in the grid
  public rowData$!: Observable<any[]>;

  // For accessing the Grid's API
  @ViewChild(AgGridAngular) agGrid!: AgGridAngular;
  @ViewChild('fileInput') fileInput!: ElementRef;

  selectedFile: any;
  constructor(private oppGridService : CrmOpportunityGridServiceService,
    private dealsGridService : CrmDealsGridServiceService,
    @Inject(ChangeDetectorRef) public cdRef: ChangeDetectorRef,
    private http: HttpClient, private uploadService: CrmUploadfileServiceService,
    private datePipe: DatePipe) { }

  ngOnInit() {
    // this.PopulateOppGridData();
    // this.PopulateDealsGridData();
  }

  handleCellValueChanged(event: CellValueChangedEvent) {
    // Your custom logic to handle the cell value changed event here
    console.log('Cell value changed from' + event.oldValue + ' to '+ event.newValue);
  }
  openSlider() {
    this.sliderOpen = true;
    this.cdRef && this.cdRef.detectChanges();
  }

  onSaveSlider(data: any) {
    console.log(data);
    this.sliderOpen = false;
    this.cdRef && this.cdRef.detectChanges();
    // Add the data to your grid's data source
    //this.gridData.push(data);
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

  onGridReady(params: GridReadyEvent) {
    this.gridApi = params.api;
    this.gridColumnApi = params.columnApi; 
    this.gridApi.showLoadingOverlay();   
    this.rowData$ = this.http
      .get<any[]>('http://localhost:5109/api/opportunity/getAllOpportunity');    
  }

  // Example of consuming Grid Event
  onCellClicked( e: CellClickedEvent): void {
    console.log('cellClicked', e);
  }

  getGridHeight() {
    const rowHeight = this.gridApi?.getRowHeight() || 25; // Use a default row height if needed
    return (this.rowsPerPage * rowHeight) + 'px';
  }
 
  onCellValueChanged(params){
    console.log(params);
  }
  onGridSizeChanged(params){
    params.api.sizeColumnsToFit();
  }

  onFileSelected(event: any): void {
    this.selectedFile = event.target.files[0];
  }

  uploadFile(): void {
    const formData = new FormData();
    formData.append('file', this.selectedFile);
  
    this.uploadService.uploadFile(formData).subscribe(
      (response) => {
        console.log('File uploaded successfully.');
        this.PopulateContactsGridData();
        this.fileInput.nativeElement.value = '';
      },
      (error) => {
        console.error('Error uploading file:', error);
      }
    );
  }
  
  PopulateContactsGridData(){
    this.http
      .get<any[]>('http://localhost:5109/api/contacts/getAllContacts').subscribe(
        (resp)=>{ this.gridApi && this.gridApi.setRowData(resp)},
        (error)=>{ console.log(error)}
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

