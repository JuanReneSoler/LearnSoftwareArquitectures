import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TasksByPersonComponent } from './tasks-by-person.component';

describe('TasksByPersonComponent', () => {
  let component: TasksByPersonComponent;
  let fixture: ComponentFixture<TasksByPersonComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TasksByPersonComponent]
    });
    fixture = TestBed.createComponent(TasksByPersonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
