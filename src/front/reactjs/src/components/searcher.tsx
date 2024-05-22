import { ChangeEvent, useContext, useState } from "react"
import { AppContext } from "../contexts";

export const Searcher = ()=>{
    const [text, setText]=useState("");
    const {shareSearchText}=useContext(AppContext);

    const handlerChangeEvent = (e:ChangeEvent<HTMLInputElement>)=>{
        e.preventDefault();
        const {value}=e.target;
        setText(value);
    }

    const handlerClearEvent = ()=>{
        setText("");
        shareSearchText("");
    }

    return <div>
        <input type="text" name="text" value={text} placeholder="Filtro" onChange={handlerChangeEvent} title="Filtro global de busqueda" />
        <button onClick={()=>shareSearchText(text)} type="button">Buscar</button>
        <button type="button" onClick={handlerClearEvent}>limpiar</button>
    </div>
}