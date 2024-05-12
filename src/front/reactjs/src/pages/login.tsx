import { useContext } from "react";
import { SesionContext } from "../modules/auth";

const Login = () => {
  const { logIn } = useContext(SesionContext);

  const loguearseHandler = () => {
    logIn("juan", "123");
  };
  return (
    <form>
      <p>LogIn</p>
      <div>
        <label htmlFor="">Usuario</label>
        <input type="text" placeholder="ponga el usuario" />
      </div>
      <div>
        <label htmlFor="">Contraseña</label>
        <input type="password" placeholder="ponga la contraseña" />
      </div>
      <div>
        <button onClick={loguearseHandler}>Logearse</button>
      </div>
    </form>
  );
};

export { Login };
