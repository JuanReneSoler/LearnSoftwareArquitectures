import { Component, OnInit } from '@angular/core';
import { GroupService } from 'src/app/services/group.service';
import { PeopleService } from 'src/app/services/people.service';

@Component({
  selector: 'app-task-form',
  templateUrl: './task-form.component.html',
  styleUrls: ['./task-form.component.css'],
})
export class TaskFormComponent implements OnInit {
  constructor(
    private peopleService: PeopleService,
    private gruposSerivce: GroupService
  ) {}

  groupsList = [] as any[];
  peopleList = [] as any[];
  token: string = `${localStorage.getItem('token')}`;

  async ngOnInit() {
    await this.gruposSerivce.filter(this.token).then((res) => {
      this.groupsList = res;
    });

    await this.peopleService.filter(this.token).then((res) => {
      this.peopleList = res;
    });
  }
}
