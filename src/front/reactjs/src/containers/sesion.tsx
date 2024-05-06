import { createContext, useState } from "react";

interface IProps {
  children: JSX.Element;
}

interface Sesion {
  isLogged: boolean;
  logIn: (user: string, password: string) => void;
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
  return (
    <SesionContext.Provider value={{ isLogged, logIn }}>
      {children}
    </SesionContext.Provider>
  );
};

export { SesionContext, SesionProvider };
