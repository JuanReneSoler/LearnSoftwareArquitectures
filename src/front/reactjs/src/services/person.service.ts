import { Person } from ".";

const api = import.meta.env.VITE_API + "Person";

const PersonService = {
  Add: async (dto: Person, abort?: AbortController) => {
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
  Update: async (dto: Person, abort?: AbortController) => {
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
export { PersonService };
