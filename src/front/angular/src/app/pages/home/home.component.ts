import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {
  showTaskList = true;
  showGroupList = false;
  showPersonList = false;

  public viewTaskList(): void {
    this.showTaskList = true;
    this.showPersonList = false;
    this.showGroupList = false;
  }

  public viewPeopleList(): void {
    this.showTaskList = false;
    this.showPersonList = true;
    this.showGroupList = false;
  }

  public viewGroupsList(): void {
    this.showTaskList = false;
    this.showPersonList = false;
    this.showGroupList = true;
  }
}
