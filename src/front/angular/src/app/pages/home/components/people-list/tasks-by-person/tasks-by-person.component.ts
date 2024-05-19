import { Component, Input, OnInit } from '@angular/core';
import { TasksService } from 'src/app/services/tasks.service';

@Component({
  selector: 'app-tasks-by-person',
  templateUrl: './tasks-by-person.component.html',
  styleUrls: ['./tasks-by-person.component.css'],
})
export class TasksByPersonComponent implements OnInit {
  @Input() personId: number = 0;
  tasksList: any[] = [];

  constructor(private service: TasksService) {}

  async ngOnInit() {
    await this.service.filter({ personId: this.personId }).then((res) => {
      this.tasksList = res;
    });
  }
}
