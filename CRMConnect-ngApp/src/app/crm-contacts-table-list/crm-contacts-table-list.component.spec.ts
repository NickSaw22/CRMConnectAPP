import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrmContactsTableListComponent } from './crm-contacts-table-list.component';

describe('CrmContactsTableListComponent', () => {
  let component: CrmContactsTableListComponent;
  let fixture: ComponentFixture<CrmContactsTableListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CrmContactsTableListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CrmContactsTableListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
