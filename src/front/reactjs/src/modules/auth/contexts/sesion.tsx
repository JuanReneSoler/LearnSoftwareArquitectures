import { createContext, useState } from "react";

interface IProps {
  children: JSX.Element;
}

interface Sesion {
  isLogged: boolean;
  logIn: (user: string, password: string) => void;
  logOut:()=>void;
}

const SesionContext = createContext({
  isLogged: false,
} as Sesion);

const SesionProvider = ({ children }: IProps) => {
  const [isLogged, setIsLogged] = useState(false);
  const logIn = (user: string, password: string) => {
    if (user === "juan" && password === "123") {
      setIsLogged(true);
    }
  };
  const logOut=()=>{
    setIsLogged(false);
  }
  return (
    <SesionContext.Provider value={{ isLogged, logIn, logOut }}>
      {children}
    </SesionContext.Provider>
  );
};

export { SesionContext, SesionProvider };
