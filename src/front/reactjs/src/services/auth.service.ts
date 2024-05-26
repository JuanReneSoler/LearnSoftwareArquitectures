const api = import.meta.env.VITE_API + "Auth";

const authService = {
  login: async (task: {
    user: string;
    password: string;
  }): Promise<{ token: string }> => {
    return await fetch(api, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(task),
    }).then((res) => res.json());
  },
};
export { authService };
