import { buildUrl } from "../utils";
import { Task } from "./dtos";
import { IResponse } from "./interfaces/IResponse";

const api = import.meta.env.VITE_API + "Task";

interface IFilterParams {
  GroupId?: number;
  PersonId?: number;
  search?:string;
  page: number;
  size: number;
}

const taskService = {
  filter: async (params: IFilterParams): Promise<IResponse<Task>> => {
    const url = buildUrl(api, params);

    return await fetch(url, {
      method: "GET",
    }).then((res) => res.json());
  },

  create: async (task: Task): Promise<Task> => {
    return await fetch(api, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(task),
    }).then((res) => res.json());
  },

  delete: async (id: number): Promise<number> => {
    return await fetch(api + `/${id}`, {
      method: "DELETE",
    }).then((res) => res.json());
  },

  update: async (task: Task): Promise<Task> => {
    return await fetch(api, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(task),
    }).then((res) => res.json());
  },

  changeGroup: async (idTask: number, idGroup: number) => {
    return await fetch(api + `/${idTask}/changeGroup/${idGroup}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
    }).then((res) => res.json());
  },
};
export { taskService };
