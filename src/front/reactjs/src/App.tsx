import { useContext, useEffect } from "react";
import { SesionContext } from "./containers";
import { Home, Login } from "./pages";

function App() {
  const { isLogged } = useContext(SesionContext);

  return <>{isLogged ? <Home /> : <Login />}</>;
}

export default App;
