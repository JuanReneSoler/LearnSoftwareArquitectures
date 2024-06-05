import { Injectable } from '@angular/core';
import { Group } from './dtos/Group';
import { env } from '../../../env';

const api = env.apiUrl + 'Group';

@Injectable({
  providedIn: 'root',
})
export class GroupService {
  async create(group: Group, token: string): Promise<Group> {
    return await fetch(api, {
      method: 'POST',
      headers: {
        'Content-Type': 'application-json',
        Authorization: token,
      },
      body: JSON.stringify(group),
    }).then((res) => res.json());
  }

  async filter(token: string): Promise<Array<Group>> {
    return await fetch(api, {
      method: 'GET',
      headers: {
        Authorization: token,
      },
    }).then((res) => res.json());
  }

  async delete(id: number, token: string): Promise<number> {
    return await fetch(api + `/${id}`, {
      method: 'DELETE',
      headers: {
        Authorization: token,
      },
    }).then((res) => res.json());
  }

  async update(group: Group, token: string): Promise<Array<Group>> {
    return await fetch(api, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application-json',
        Application: token,
      },
      body: JSON.stringify(group),
    }).then((res) => res.json());
  }
}
