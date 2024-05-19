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

  async ngOnInit() {
    await this.gruposSerivce.filter().then((res) => {
      this.groupsList = res;
    });

    await this.peopleService.filter().then((res) => {
      this.peopleList = res;
    });
  }
}
