import { Task } from ".";

const api = import.meta.env.VITE_API + "Task";

const TaskService = {
  Add: async (dto: Task, abort?: AbortController) => {
    return await fetch(api, {
      method: "post",
      signal: abort?.signal,
      body: JSON.stringify(dto),
    }).then((res) => res.json());
  },
  Get: async (Id: number, abort?: AbortController) => {
    return await fetch(api + `/${Id}`, {
      method: "get",
      signal: abort?.signal,
    }).then((res) => res.json());
  },
  List: async (abort?: AbortController) => {
    return await fetch(api, {
      method: "get",
      signal: abort?.signal,
    }).then((res) => res.json());
  },
  Delete: async (Id: number, abort?: AbortController) => {
    return await fetch(api + `/${Id}`, {
      method: "delete",
      signal: abort?.signal,
    }).then((res) => res.json());
  },
  Update: async (dto: Task, abort?: AbortController) => {
    return await fetch(api, {
      method: "put",
      signal: abort?.signal,
      body: JSON.stringify(dto),
    }).then((res) => res.json());
  },
};
export { TaskService };
