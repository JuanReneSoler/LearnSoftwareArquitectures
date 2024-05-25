import { useState } from "react";
import { Tasks } from "./Tasks";

interface IProps {
  item: { id: number; name: string };
  handlerSelect: (id: number) => void;
  handlerDelete: (id: number) => void;
}
export const CustonLi = ({ item, handlerDelete, handlerSelect }: IProps) => {
  const [showTasks, setShowTasks] = useState(false);
  return (
    <li>
      {item.name}-
      <a
        href="#"
        onClick={() => {
          handlerSelect(item.id);
        }}
      >
        ( ver )
      </a>
      -
      <a href="#" onClick={() => handlerDelete(item.id)}>
        ( eliminar )
      </a>
      {!showTasks && (
        <>
          -
          <a href="#" onClick={() => setShowTasks(true)}>
            ( ver tareas )
          </a>
        </>
      )}
      {showTasks && <Tasks groupId={item.id ?? 0} />}
      <br />
    </li>
  );
};
