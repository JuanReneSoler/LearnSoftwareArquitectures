import { Component, OnInit } from '@angular/core';
import { TasksService } from 'src/app/services/tasks.service';

@Component({
  selector: 'app-tasks-management',
  templateUrl: './tasks-management.component.html',
  styleUrls: ['./tasks-management.component.css'],
})
export class TasksManagementComponent implements OnInit {
  taskList = [] as any[];
  showForm = false;
  token: string = `${localStorage.getItem('token')}`;

  constructor(private service: TasksService) {
    //
  }
  async ngOnInit() {
    await this.service.filter({ page: 1, size: 10 }, this.token).then((res) => {
      this.taskList = res;
    });
  }
}
