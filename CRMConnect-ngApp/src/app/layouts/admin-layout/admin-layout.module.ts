import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AdminLayoutRoutes } from './admin-layout.routing';
import { DashboardComponent } from '../../dashboard/dashboard.component';
import { UserProfileComponent } from '../../user-profile/user-profile.component';
import { TableListComponent } from '../../table-list/table-list.component';
import { CrmContactsTableListComponent } from '../../crm-contacts-table-list/crm-contacts-table-list.component';
import { TypographyComponent } from '../../typography/typography.component';
import { IconsComponent } from '../../icons/icons.component';
import { MapsComponent } from '../../maps/maps.component';
import { NotificationsComponent } from '../../notifications/notifications.component';
import { UpgradeComponent } from '../../upgrade/upgrade.component';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatRippleModule} from '@angular/material/core';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatSelectModule} from '@angular/material/select';
import { AgGridModule } from 'ag-grid-angular';
import {CrmOpportunityGridServiceService} from '../../services/crm-opportunity-grid-service.service'
import {CrmDealsGridServiceService} from '../../services/crm-deals-grid-service.service'
import {CrmChartDataServiceService} from '../../services/crm-chart-data-service.service'
import {CrmUploadfileServiceService} from '../../services/crm-uploadfile-service.service'
import {CrmCustomGridDropdownEditorComponent} from '../../crm-custom-grid-dropdown-editor/crm-custom-grid-dropdown-editor.component'
import { CrmOpportunityTableListComponent } from 'app/crm-opportunity-table-list/crm-opportunity-table-list.component';
import { DatePipe } from '@angular/common';
import { CrmAddOpportunityRowSliderComponent } from 'app/crm-add-opportunity-row-slider/crm-add-opportunity-row-slider.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AdminLayoutRoutes),
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatRippleModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatTooltipModule,
    AgGridModule,    
  ],
  declarations: [
    DashboardComponent,
    UserProfileComponent,
    TableListComponent,
    TypographyComponent,
    IconsComponent,
    MapsComponent,
    NotificationsComponent,
    UpgradeComponent,
    CrmContactsTableListComponent,
    CrmCustomGridDropdownEditorComponent,
    CrmOpportunityTableListComponent,    
    CrmAddOpportunityRowSliderComponent,
  ],
  providers: [
    CrmOpportunityGridServiceService,
    CrmDealsGridServiceService,
    CrmUploadfileServiceService,
    CrmChartDataServiceService,
    DatePipe
  ],
})

export class AdminLayoutModule {}
