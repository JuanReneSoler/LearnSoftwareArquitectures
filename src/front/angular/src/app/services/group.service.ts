import { Injectable } from '@angular/core';
import { Group } from './dtos/Group';
import { env } from '../../../env';

const api = env.apiUrl + 'Group';

@Injectable({
  providedIn: 'root',
})
export class GroupService {
  async create(group: Group): Promise<Group> {
    return await fetch(api, {
      method: 'POST',
      headers: {
        'Content-Type': 'application-json',
      },
      body: JSON.stringify(group),
    }).then((res) => res.json());
  }
  async filter(): Promise<Array<Group>> {
    return await fetch(api, {
      method: 'GET',
    }).then((res) => res.json());
  }
  async delete(id: number): Promise<number> {
    return await fetch(api, {
      method: 'GET',
    }).then((res) => res.json());
  }
  async update(group: Group): Promise<Array<Group>> {
    return await fetch(api, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application-json',
      },
      body: JSON.stringify(group),
    }).then((res) => res.json());
  }
}
