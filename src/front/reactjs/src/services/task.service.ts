import { Task } from ".";

const api = import.meta.env.VITE_API + "Task";

const TaskService = {
  Add: async (dto: Task) => {
    return await fetch(api, {
      method: "post",
      body: JSON.stringify(dto),
    }).then((res) => res.json());
  },
  Get: async (Id: number) => {
    return await fetch(api + `/${Id}`, {
      method: "get",
    }).then((res) => res.json());
  },
  List: async () => {
    return await fetch(api, {
      method: "get",
    }).then((res) => res.json());
  },
  Delete: async (Id: number) => {
    return await fetch(api + `/${Id}`, {
      method: "delete",
    }).then((res) => res.json());
  },
  Update: async (dto: Task) => {
    return await fetch(api, {
      method: "put",
      body: JSON.stringify(dto),
    }).then((res) => res.json());
  },
};
export { TaskService };
