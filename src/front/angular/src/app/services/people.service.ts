import { Injectable } from '@angular/core';
import { Person } from './dtos/Person';
import { env } from '../../../env';

const api = env.apiUrl + 'Person';

@Injectable({
  providedIn: 'root',
})
export class PeopleService {
  async create(group: Person): Promise<Person> {
    return await fetch(api, {
      method: 'POST',
      headers: {
        'Content-Type': 'application-json',
      },
      body: JSON.stringify(group),
    }).then((res) => res.json());
  }

  async filter(): Promise<Array<Person>> {
    return await fetch(api, {
      method: 'GET',
    }).then((res) => res.json());
  }

  async delete(id: number): Promise<Person> {
    return await fetch(api + `/${id}`, {
      method: 'GET',
    }).then((res) => res.json());
  }

  async update(group: Person): Promise<Person> {
    return await fetch(api, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application-json',
      },
      body: JSON.stringify(group),
    }).then((res) => res.json());
  }
}
