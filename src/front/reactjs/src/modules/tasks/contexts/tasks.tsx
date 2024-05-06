import { createContext, useState } from "react";
import { Task } from "../../../services";

interface IProps{
    children:JSX.Element
}

interface Tasks{
    selectedTask:Task|null;
    shareSelectedTask:(task:Task|null)=>void;
}

const TaskContext = createContext({
    selectedTask:null
} as Tasks);

const TaskProvider = ({children}:IProps)=>{
    const [selectedTask, setSelectedTask] = useState(null as Task | null)

    const shareSelectedTask = (task:Task|null)=>{
        setSelectedTask(task);
    }

    return (<TaskContext.Provider value={{selectedTask, shareSelectedTask}}>
        {children}
    </TaskContext.Provider>)
}

export {TaskContext, TaskProvider};