import { ChangeEvent, useContext, useState } from "react"
import { AppContext } from "../contexts";

export const Searcher = ()=>{
    const [text, setText]=useState("");
    const {shareSearchText}=useContext(AppContext);

    const handlerChangeEvent = (e:ChangeEvent<HTMLInputElement>)=>{
        const {value}=e.target;
        setText(value);
    }

    return <div>
        <input type="text" name="text" value={text} placeholder="Filtro" onChange={handlerChangeEvent} />
        <button onClick={()=>shareSearchText(text)} type="button">Buscar</button>
    </div>
}