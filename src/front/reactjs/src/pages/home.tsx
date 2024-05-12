import { useContext } from "react";
import { TaskProvider } from "../modules/cruds";
import { SesionContext } from "../modules/auth";
import { Login } from "./login";
import { Main } from "./main";

const Home = () => {
  const { isLogged } = useContext(SesionContext);
  return isLogged ? (
    <TaskProvider>
      <Main />
    </TaskProvider>
  ) : (
    <Login />
  );
};
export { Home };
