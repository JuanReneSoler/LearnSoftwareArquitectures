import React, { createContext, useEffect, useState } from "react";

interface IAppContextProps
{
    globalSearchText:string;
    shareSearchText:(newText:string)=>void;
}

export const AppContext = createContext({} as IAppContextProps);

interface IProps
{
    children:React.ReactNode;
}

export const AppContextProvider = ({children}:IProps)=>{
    const [searchText, setSearchText]=useState("");

    const shareSearchText = (searchText:string)=>setSearchText(searchText);

    return (
        <AppContext.Provider value={{globalSearchText:searchText, shareSearchText}}>
            {children}
        </AppContext.Provider>
    );
}