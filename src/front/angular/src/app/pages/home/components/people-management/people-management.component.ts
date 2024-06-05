import { Component, OnInit } from '@angular/core';
import { PeopleService } from 'src/app/services/people.service';

@Component({
  selector: 'app-people-management',
  templateUrl: './people-management.component.html',
  styleUrls: ['./people-management.component.css'],
})
export class PeopleManagementComponent implements OnInit {
  constructor(private service: PeopleService) {}
  peopleList: any[] = [];
  showForm = false;
  token: string = `${localStorage.getItem('token')}`;

  async ngOnInit() {
    await this.service.filter(this.token).then((res) => {
      this.peopleList = res;
    });
  }
}
