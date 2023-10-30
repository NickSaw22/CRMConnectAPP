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

@Component({
  selector: 'app-crm-contacts-table-list',
  templateUrl: './crm-contacts-table-list.component.html',
  styleUrls: ['./crm-contacts-table-list.component.css']
})
export class CrmContactsTableListComponent implements OnInit {
  gridApi: any;
  gridColumnApi: any;
  opportunitiesDataList: any = []
  delasDataList; any = [];
  rowsPerPage = 5;
  frameworkComponents = {
    customDropdownEditor: CrmCustomGridDropdownEditorComponent,
  };
  public columnDefs: ColDef[] = [
    { field: 'firstName',  editable: false},
    { field: 'lastName',  editable: false},
    { field: 'jobTitle',  editable: false },
    { field: 'deptName',  editable: false},
    { field: 'phoneNumber',  editable: true},
    { field: 'emailAddress',  editable: true},
    {
      field: 'yourField',
      editable: true,
      cellEditor: 'agSelectCellEditor',
      cellRenderer: this.getSelectedValue.bind(this),
      cellEditorParams: {
        values:['Select Stage', 'Option 1', 'Option 2', 'Option 3', 'Option 4', 'Option 5'],
        handleGridCellEvent: this.handleCellValueChanged.bind(this),
      },
    }
  ];

  // DefaultColDef sets props common to all Columns
  public defaultColDef: ColDef = {
    sortable: true,
    filter: true,   
    resizable: true
  };
  getSelectedValue(params: ICellRendererParams): string {
    return params.value ? 
      params.value :
      (['Failed', 'Completed'].includes(params.data.status)) && !params.value ? 'Selecte Stage' : '';
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
    private http: HttpClient, private uploadService: CrmUploadfileServiceService) { }

  ngOnInit() {
    // this.PopulateOppGridData();
    // this.PopulateDealsGridData();
  }

  handleCellValueChanged(event: CellValueChangedEvent) {
    // Your custom logic to handle the cell value changed event here
    console.log('Cell value changed:', event);
  }
  customDropdownCellRenderer(params) {
    const select = document.createElement('select');
    const options = params.colDef.cellEditorParams.options;
    options.forEach(option => {
      const optionElement = document.createElement('option');
      optionElement.value = option.value;
      optionElement.text = option.label;
      select.appendChild(optionElement);
    });
  
    select.value = params.value;
    select.addEventListener('change', (event) => {
      params.node.setDataValue(params.colDef.field, event.target['value']);
    });
  
    return select;
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
      .get<any[]>('http://localhost:5109/api/contacts/getAllContacts');    
  }

  // Example of consuming Grid Event
  onCellClicked( e: CellClickedEvent): void {
    console.log('cellClicked', e);
  }

  getGridHeight() {
    const rowHeight = this.gridApi?.getRowHeight() || 25; // Use a default row height if needed
    return (this.rowsPerPage * rowHeight) + 'px';
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
