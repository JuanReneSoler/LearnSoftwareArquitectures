import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskListComponent } from './components/task-list/task-list.component';
import { RouterModule, Routes } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';

const routes: Routes = [
  {
    path: 'task-list',
    component: TaskListComponent,
  },
];

@NgModule({
  declarations: [TaskListComponent],
  exports: [RouterModule],
  imports: [CommonModule, BrowserModule, RouterModule.forRoot(routes)],
})
export class TasksModule {}
