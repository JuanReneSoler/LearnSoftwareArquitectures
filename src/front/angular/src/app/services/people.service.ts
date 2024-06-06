import { Injectable } from '@angular/core';
import { Person } from './dtos/Person';
import { env } from '../../../env';
import { buildUrl } from '../utils/buildUrl';

const api = env.apiUrl + 'Person';

@Injectable({
  providedIn: 'root',
})
export class PeopleService {
  async create(group: Person, token: string): Promise<Person> {
    return await fetch(api, {
      method: 'POST',
      headers: {
        'Content-Type': 'application-json',
        Authorization: token,
      },
      body: JSON.stringify(group),
    }).then((res) => res.json());
  }

  async filter(
    params: { page: number; size: number },
    token: string
  ): Promise<Array<Person>> {
    const url = buildUrl(api, params);
    return await fetch(url, {
      method: 'GET',
      headers: {
        Authorization: token,
      },
    }).then((res) => res.json());
  }

  async delete(id: number, token: string): Promise<Person> {
    return await fetch(api + `/${id}`, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application-json',
        Authorization: token,
      },
    }).then((res) => res.json());
  }

  async update(group: Person, token: string): Promise<Person> {
    return await fetch(api, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application-json',
        Authorization: token,
      },
      body: JSON.stringify(group),
    }).then((res) => res.json());
  }
}
