import { Injectable } from '@angular/core';
import { Task } from './dtos/Task';
import { env } from '../../../env';
import { buildUrl } from '../utils/buildUrl';

const api = env.apiUrl + 'Task';

interface IFilterProps {
  groupId?: number;
  personId?: number;
}

@Injectable({
  providedIn: 'root',
})
export class TasksService {
  async create(group: Task): Promise<Task> {
    return await fetch(api, {
      method: 'POST',
      headers: {
        'Content-Type': 'application-json',
      },
      body: JSON.stringify(group),
    }).then((res) => res.json());
  }

  async filter(params?: IFilterProps): Promise<Array<Task>> {
    const url = buildUrl(api, params);
    return await fetch(url, {
      method: 'GET',
    }).then((res) => res.json());
  }

  async delete(id: number): Promise<number> {
    return await fetch(api + `/${id}`, {
      method: 'GET',
    }).then((res) => res.json());
  }

  async update(group: Task): Promise<Task> {
    return await fetch(api, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application-json',
      },
      body: JSON.stringify(group),
    }).then((res) => res.json());
  }

  async changeGroup(id: number, idGroup: number) {
    return await fetch(api + `/${id}/ChangeGroup/${idGroup}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application-json',
      },
    }).then((res) => res.json());
  }
}
