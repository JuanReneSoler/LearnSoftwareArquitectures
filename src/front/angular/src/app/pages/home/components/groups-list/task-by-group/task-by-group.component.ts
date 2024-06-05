import { Component, Input, OnInit } from '@angular/core';
import { TasksService } from 'src/app/services/tasks.service';

@Component({
  selector: 'app-task-by-group',
  templateUrl: './task-by-group.component.html',
  styleUrls: ['./task-by-group.component.css'],
})
export class TaskByGroupComponent implements OnInit {
  @Input() groupId: number = 0;
  tasksList: any[] = [];

  token: string = `${localStorage.getItem('token')}`;

  constructor(private service: TasksService) {}

  async ngOnInit() {
    await this.service
      .filter({ groupId: this.groupId }, this.token)
      .then((res) => {
        this.tasksList = res;
      });
  }
}
