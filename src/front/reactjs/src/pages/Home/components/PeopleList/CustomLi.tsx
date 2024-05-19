import { useState } from "react";
import { Tasks } from "./Tasks";

interface IProps {
  item: { id: number; name: string };
  key: number;
  handlerSelect: (id: number) => void;
  handlerDelete: (id: number) => void;
}
export const CustonLi = ({
  item,
  key,
  handlerDelete,
  handlerSelect,
}: IProps) => {
  const [showTasks, setShowTasks] = useState(false);
  return (
    <li key={key}>
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
      {showTasks && <Tasks personId={item.id ?? 0} />}
      <br />
    </li>
  );
};
