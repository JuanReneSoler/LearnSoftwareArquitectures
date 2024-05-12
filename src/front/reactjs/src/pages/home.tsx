import { useContext } from "react";
import { SesionContext } from "../modules/auth";
import { Login } from "./login";
import { Main } from "./main";
import { TaskProvider } from "../modules/cruds";

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
