import { Group } from "./dtos";

const api = import.meta.env.VITE_API + "Group";

const groupService = {
  filter: async (): Promise<Array<Group>> => {
    return await fetch(api, {
      method: "GET",
    }).then((res) => res.json());
  },

  create: async (task: Group): Promise<Group> => {
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

  update: async (task: Group): Promise<Group> => {
    return await fetch(api, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(task),
    }).then((res) => res.json());
  },
};
export { groupService };
