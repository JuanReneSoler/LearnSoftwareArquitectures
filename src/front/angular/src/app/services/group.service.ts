import { Injectable } from '@angular/core';
import { apiUrl } from '../../../env.json';
import { Group } from './dtos/Group';

const api = apiUrl + 'Group';

@Injectable({
  providedIn: 'root',
})
export class GroupService {
  constructor() {}
  async create(group: Group) {
    return await fetch(api, {
      method: 'POST',
      headers: {
        'Content-Type': 'application-json',
      },
      body: JSON.stringify(group),
    }).then((res) => res.json());
  }
  async filter() {
    await fetch(api, {
      method: 'GET',
    }).then((res) => res.json());
  }
  async delete(id: number) {
    await fetch(api, {
      method: 'GET',
    }).then((res) => res.json());
  }
  async update(group: Group) {
    return await fetch(api, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application-json',
      },
      body: JSON.stringify(group),
    }).then((res) => res.json());
  }
}
