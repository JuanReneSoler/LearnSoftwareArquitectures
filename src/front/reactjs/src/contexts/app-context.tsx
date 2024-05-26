import React, { createContext, useState } from "react";

interface IAppContextProps {
  globalSearchText: string;
  shareSearchText: (newText: string) => void;
  token: string;
  setToken: (token: string) => void;
}

export const AppContext = createContext({} as IAppContextProps);

interface IProps {
  children: React.ReactNode;
}

export const AppContextProvider = ({ children }: IProps) => {
  const [searchText, setSearchText] = useState("");
  const [token, setToken] = useState("");

  const shareSearchText = (searchText: string) => setSearchText(searchText);

  return (
    <AppContext.Provider
      value={{ globalSearchText: searchText, shareSearchText, token, setToken }}
    >
      {children}
    </AppContext.Provider>
  );
};

