import { Person } from "./dtos";

const api = import.meta.env.VITE_API + "Person";

const peopleService = {
  filter: async (): Promise<Array<Person>> => {
    return await fetch(api, {
      method: "GET",
    }).then((res) => res.json());
  },

  create: async (task: Person): Promise<Person> => {
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

  update: async (task: Person): Promise<Person> => {
    return await fetch(api, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(task),
    }).then((res) => res.json());
  },
};
export { peopleService };
