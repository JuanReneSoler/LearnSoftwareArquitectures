import { Task } from "../dtos";

const api = import.meta.env.VITE_API + "Task";

const TaskService = {
  Add: async (dto: Task, abort?: AbortController) => {
    return await fetch(api, {
      method: "post",
      headers: {
        "Content-Type": "application/json",
      },
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

  List: async (groupId: number | null, abort?: AbortController) => {
    return await fetch(api + `/${groupId ? `?GroupId=${groupId}` : ""}`, {
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
      headers: {
        "Content-Type": "application/json",
      },
      signal: abort?.signal,
      body: JSON.stringify(dto),
    }).then((res) => res.json());
  },
};
export { TaskService };
