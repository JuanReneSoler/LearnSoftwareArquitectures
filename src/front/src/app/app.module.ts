import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { RouterModule, Routes } from '@angular/router';
import { TaskListComponent } from './components/task-list/task-list.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
  },
  {
    path: '**',
    redirectTo: '/notfound',
  },
];

@NgModule({
  declarations: [AppComponent, TaskListComponent],
  exports: [RouterModule],
  imports: [BrowserModule, RouterModule.forRoot(routes)],
  bootstrap: [AppComponent],
})
export class AppModule {}
