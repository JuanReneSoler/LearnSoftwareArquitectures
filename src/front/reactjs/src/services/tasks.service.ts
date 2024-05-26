import { buildUrl } from "../utils";
import { Task } from "./dtos";
import { IResponse } from "./interfaces/IResponse";

const api = import.meta.env.VITE_API + "Task";

interface IFilterParams {
  GroupId?: number;
  PersonId?: number;
  search?: string;
  page: number;
  size: number;
}

const taskService = {
  filter: async (
    params: IFilterParams,
    token: string
  ): Promise<IResponse<Task>> => {
    const url = buildUrl(api, params);

    return await fetch(url, {
      method: "GET",
      headers: {
        Authorization: token,
      },
    }).then((res) => res.json());
  },

  create: async (task: Task, token: string): Promise<Task> => {
    return await fetch(api, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: token,
      },
      body: JSON.stringify(task),
    }).then((res) => res.json());
  },

  delete: async (id: number, token: string): Promise<number> => {
    return await fetch(api + `/${id}`, {
      method: "DELETE",
      headers: {
        Authorization: token,
      },
    }).then((res) => res.json());
  },

  update: async (task: Task, token: string): Promise<Task> => {
    return await fetch(api, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: token,
      },
      body: JSON.stringify(task),
    }).then((res) => res.json());
  },

  changeGroup: async (idTask: number, idGroup: number, token: string) => {
    return await fetch(api + `/${idTask}/changeGroup/${idGroup}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: token,
      },
    }).then((res) => res.json());
  },
};
export { taskService };
