import { Component, Input, OnInit } from '@angular/core';
import { TasksService } from 'src/app/services/tasks.service';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css'],
})
export class TasksComponent implements OnInit {
  @Input() groupId: number = 0;
  taskList: any[] = [];

  constructor(private service: TasksService) {}

  async ngOnInit() {
    await this.service.filter({ groupId: this.groupId }).then((res) => {
      this.taskList = res;
    });
  }
}
