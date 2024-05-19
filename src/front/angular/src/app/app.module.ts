import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { RouterModule, Routes } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { HomeComponent } from './pages/home/home.component';
import { TaskListComponent } from './pages/home/components/task-list/task-list.component';
import { TaskFormComponent } from './pages/home/components/task-form/task-form.component';
import { GroupFormComponent } from './pages/home/components/group-form/group-form.component';
import { GroupsListComponent } from './pages/home/components/groups-list/groups-list.component';
import { PeopleListComponent } from './pages/home/components/people-list/people-list.component';
import { PersonFormComponent } from './pages/home/components/person-form/person-form.component';
import { PeopleManagementComponent } from './pages/home/components/people-management/people-management.component';
import { TasksManagementComponent } from './pages/home/components/tasks-management/tasks-management.component';
import { GroupsManagementComponent } from './pages/home/components/groups-management/groups-management.component';
import { TaskByGroupComponent } from './pages/home/components/groups-list/task-by-group/task-by-group.component';
import { TasksByPersonComponent } from './pages/home/components/people-list/tasks-by-person/tasks-by-person.component';

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
    TaskByGroupComponent,
    TasksByPersonComponent,
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
