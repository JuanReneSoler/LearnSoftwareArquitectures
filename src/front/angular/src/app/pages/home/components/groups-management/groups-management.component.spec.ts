import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupsManagementComponent } from './groups-management.component';

describe('GroupsManagementComponent', () => {
  let component: GroupsManagementComponent;
  let fixture: ComponentFixture<GroupsManagementComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GroupsManagementComponent]
    });
    fixture = TestBed.createComponent(GroupsManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
