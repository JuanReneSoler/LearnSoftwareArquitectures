import { Component, OnInit } from '@angular/core';
import { GroupService } from 'src/app/services/group.service';

@Component({
  selector: 'app-groups-management',
  templateUrl: './groups-management.component.html',
  styleUrls: ['./groups-management.component.css'],
})
export class GroupsManagementComponent implements OnInit {
  groupsList: any[] = [];
  showForm = false;
  token: string = `${localStorage.getItem('token')}`;

  constructor(private service: GroupService) {}

  async ngOnInit() {
    await this.service.filter(this.token).then((res) => {
      this.groupsList = res;
    });
  }
}
