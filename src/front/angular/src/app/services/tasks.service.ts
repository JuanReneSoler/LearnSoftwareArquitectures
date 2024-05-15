import { Injectable } from '@angular/core';
import { Task } from './dtos/Task';
import { env } from '../../../env';

const api = env.apiUrl + 'Task';

@Injectable({
  providedIn: 'root',
})
export class TasksService {
  constructor() {}
  async create(group: Task): Promise<Task> {
    return await fetch(api, {
      method: 'POST',
      headers: {
        'Content-Type': 'application-json',
      },
      body: JSON.stringify(group),
    }).then((res) => res.json());
  }
  async filter(): Promise<Array<Task>> {
    return await fetch(api, {
      method: 'GET',
    }).then((res) => res.json());
  }
  async delete(id: number): Promise<number> {
    return await fetch(api, {
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
}
