import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-people-list',
  templateUrl: './people-list.component.html',
  styleUrls: ['./people-list.component.css'],
})
export class PeopleListComponent {
  @Input() peopleList: any[] = [];
  @Input() readonly: boolean = false;
}
