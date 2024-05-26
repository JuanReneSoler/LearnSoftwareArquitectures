import { buildUrl } from "../utils";
import { Group } from "./dtos";
import { IResponse } from "./interfaces/IResponse";

const api = import.meta.env.VITE_API + "Group";

interface IFilterParams {
  page: number;
  size: number;
  search?: string;
}

const groupService = {
  filter: async (
    params: IFilterParams,
    token: string
  ): Promise<IResponse<Group>> => {
    const url = buildUrl(api, params);
    return await fetch(url, {
      method: "GET",
      headers: {
        Authorization: token,
      },
    }).then((res) => res.json());
  },

  create: async (task: Group, token: string): Promise<Group> => {
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

  update: async (task: Group, token: string): Promise<Group> => {
    return await fetch(api, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: token,
      },
      body: JSON.stringify(task),
    }).then((res) => res.json());
  },
};
export { groupService };
