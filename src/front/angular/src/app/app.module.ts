import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { RouterModule, Routes } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { HomeComponent } from './pages/main/home/home.component';
import { TaskListComponent } from './pages/tasks/task-list/task-list.component';
import { TaskFormComponent } from './pages/tasks/task-form/task-form.component';
import { GroupFormComponent } from './pages/groups/group-form/group-form.component';
import { GroupsListComponent } from './pages/groups/groups-list/groups-list.component';
import { PeopleListComponent } from './pages/people/people-list/people-list.component';
import { PersonFormComponent } from './pages/people/person-form/person-form.component';
import { PeopleManagementComponent } from './pages/main/people-management/people-management.component';
import { TasksManagementComponent } from './pages/main/tasks-management/tasks-management.component';
import { GroupsManagementComponent } from './pages/main/groups-management/groups-management.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    pathMatch: 'full',
  },
  {
    path: '**',
    redirectTo: '/notfound',
  },
];

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    GroupsListComponent,
    TaskListComponent,
    TaskFormComponent,
    GroupFormComponent,
    GroupsListComponent,
    PeopleListComponent,
    PersonFormComponent,
    PeopleManagementComponent,
    TasksManagementComponent,
    GroupsManagementComponent,
  ],
  exports: [RouterModule],
  imports: [
    BrowserModule,
    RouterModule.forRoot(routes),
    BrowserAnimationsModule,
    MatIconModule,
    MatButtonModule,
    MatToolbarModule,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
