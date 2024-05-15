import { Injectable } from '@angular/core';
import { apiUrl } from '../../../env.json';
import { Person } from './dtos/Person';

const api = apiUrl + 'Person';

@Injectable({
  providedIn: 'root',
})
export class PeopleService {
  constructor() {}
  async create(group: Person) {
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
  async update(group: Person) {
    return await fetch(api, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application-json',
      },
      body: JSON.stringify(group),
    }).then((res) => res.json());
  }
}
