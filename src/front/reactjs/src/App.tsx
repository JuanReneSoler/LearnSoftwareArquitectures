import { useContext } from "react";
import { Home, TaskProvider } from "./modules/tasks";
import { Login, SesionContext } from "./modules/auth";

const Tasks = ()=>{
  return (
  <TaskProvider>
    <Home />
  </TaskProvider>)
}

function App() {
  const { isLogged } = useContext(SesionContext);

  return <>{isLogged ? <Tasks /> : <Login />}</>;
}

export default App;
