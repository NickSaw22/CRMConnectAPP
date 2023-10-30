// custom-dropdown-editor.component.ts
import { Component } from '@angular/core';

@Component({
  selector: 'app-crm-custom-grid-dropdown-editor',
  templateUrl: './crm-custom-grid-dropdown-editor.component.html',
  styleUrls: ['./crm-custom-grid-dropdown-editor.component.css']
})
export class CrmCustomGridDropdownEditorComponent {
  value: any;
  options: any[];

  agInit(params: any): void {
    this.value = params.value;
    this.options = params.options;
  }

  getValue(): any {
    return this.value;
  }
}

