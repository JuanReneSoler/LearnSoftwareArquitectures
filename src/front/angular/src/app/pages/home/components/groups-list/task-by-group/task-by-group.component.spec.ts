import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskByGroupComponent } from './task-by-group.component';

describe('TaskByGroupComponent', () => {
  let component: TaskByGroupComponent;
  let fixture: ComponentFixture<TaskByGroupComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TaskByGroupComponent]
    });
    fixture = TestBed.createComponent(TaskByGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
