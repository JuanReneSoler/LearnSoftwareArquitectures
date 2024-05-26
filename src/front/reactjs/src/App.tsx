import { useContext } from "react";
import { Home } from "./pages/Home";
import { AppContext } from "./contexts";
import { Auth } from "./pages/Auth";

function App() {
  const { token } = useContext(AppContext);
  return (
    <>
      {token !== "" && <Home />}
      {token === "" && <Auth />}
    </>
  );
}

export default App;
