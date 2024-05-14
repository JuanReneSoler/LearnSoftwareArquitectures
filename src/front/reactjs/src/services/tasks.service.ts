import { Task } from "./dtos";

const api = import.meta.env.VITE_API + "Task";

const taskService = {
  filter: async (grupoId?: number): Promise<Array<Task>> => {
    return await fetch(api + `/${grupoId ? `?GroupId=${grupoId}` : ""}`, {
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
};
export { taskService };
