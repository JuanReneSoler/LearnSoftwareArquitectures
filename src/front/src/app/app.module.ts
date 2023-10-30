import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { RouterModule, Routes } from '@angular/router';
import { TasksModule } from './modules/tasks/tasks.module';

const routes: Routes = [
  {
    path: '',
    redirectTo: '/task-list',
    pathMatch: 'full',
  },
  {
    path: '**',
    redirectTo: '/notfound',
  },
];

@NgModule({
  declarations: [AppComponent],
  exports: [RouterModule],
  imports: [BrowserModule, RouterModule.forRoot(routes), TasksModule],
  bootstrap: [AppComponent],
})
export class AppModule {}
