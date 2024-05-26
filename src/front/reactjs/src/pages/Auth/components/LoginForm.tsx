import { ChangeEvent, FormEvent, useContext, useState } from "react";
import { authService } from "../../../services";
import { AppContext } from "../../../contexts";

function LoginForm() {
  const { setToken } = useContext(AppContext);

  const [viewModel, setViewModel] = useState({ user: "", password: "" } as {
    user: string;
    password: string;
  });

  const handlerChange = (e: ChangeEvent<HTMLInputElement>) => {
    e.preventDefault();
    const { name, value } = e.target;
    setViewModel({ ...viewModel, [name]: value });
  };

  const handleSubmit = (e: FormEvent) => {
    e.preventDefault();
    (async () => {
      await authService.login(viewModel).then((res) => {
        setToken(`Bearer ${res.token}`);
      });
    })();
  };

  return (
    <form onSubmit={handleSubmit}>
      <p>LogIn</p>
      <div>
        <label htmlFor="">Usuario:</label>
        <input
          type="text"
          name="user"
          placeholder="user"
          onChange={handlerChange}
        />
      </div>
      <div>
        <label htmlFor="">Contraseña:</label>
        <input
          type="password"
          name="password"
          onChange={handlerChange}
          placeholder="contraseña"
        />
      </div>
      <div>
        <button type="submit">LogIn</button>
      </div>
    </form>
  );
}

export default LoginForm;
