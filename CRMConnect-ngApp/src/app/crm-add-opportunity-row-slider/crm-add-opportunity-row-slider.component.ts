import { ChangeDetectorRef, Component, EventEmitter, Inject, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-crm-add-opportunity-row-slider',
  templateUrl: './crm-add-opportunity-row-slider.component.html',
  styleUrls: ['./crm-add-opportunity-row-slider.component.css']
})
export class CrmAddOpportunityRowSliderComponent implements OnInit {
  name:'';
  accountId:''
  contactId:''
  stage:''
  closingDate:''
  amount:''
  // Add more fields as needed
  @Input() sliderOpen: boolean = false;  
  @Output() save = new EventEmitter<any>();
  constructor(@Inject(ChangeDetectorRef) public cdRef: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.cdRef && this.cdRef.detectChanges();
  }
  onCancel(event){
    this.sliderOpen = false;
    this.cdRef && this.cdRef.detectChanges();
  }
  onSaveClick() {
    const rowData = {
      // field1: this.field1,
      // field2: this.field2
      // Map other fields accordingly
    };
    this.save.emit(rowData);
  }

}
