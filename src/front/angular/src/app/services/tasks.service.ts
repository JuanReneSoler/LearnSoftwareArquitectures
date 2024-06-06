import { Injectable } from '@angular/core';
import { Task } from './dtos/Task';
import { env } from '../../../env';
import { buildUrl } from '../utils/buildUrl';

const api = env.apiUrl + 'Task';

interface IFilterProps {
  page: number;
  size: number;
  groupId?: number;
  personId?: number;
}

@Injectable({
  providedIn: 'root',
})
export class TasksService {
  async create(group: Task, token: string): Promise<Task> {
    return await fetch(api, {
      method: 'POST',
      headers: {
        'Content-Type': 'application-json',
        Authorization: token,
      },
      body: JSON.stringify(group),
    }).then((res) => res.json());
  }

  async filter(params: IFilterProps, token: string): Promise<Array<Task>> {
    const url = buildUrl(api, params);
    return await fetch(url, {
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

  async update(group: Task, token: string): Promise<Task> {
    return await fetch(api, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application-json',
        Authorization: token,
      },
      body: JSON.stringify(group),
    }).then((res) => res.json());
  }

  async changeGroup(id: number, idGroup: number, token: string) {
    return await fetch(api + `/${id}/ChangeGroup/${idGroup}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application-json',
        Authorization: token,
      },
    }).then((res) => res.json());
  }
}
