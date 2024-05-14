import { taskService } from "../../services";
import { Task, TasksList } from "../Tasks";
import { useEffect, useState } from "react";

export interface Group {
  id: number;
  name: string;
}

interface IProps {
  items: Array<Group>;
  selectEvent?: (id: number) => void;
  deleteEvent?: (id: number) => void;
}

interface TasksProps {
  groupId?: number;
}

const Tasks = ({ groupId }: TasksProps) => {
  const [items, setItems] = useState([] as Array<Task>);

  useEffect(() => {
    (async () => {
      await taskService.filter(groupId).then((res) => setItems(res));
    })();
  }, [groupId]);

  return <TasksList readonly={true} items={items} />;
};

function GroupsList({ items, deleteEvent, selectEvent }: IProps) {
  const handlerDelete = (id: number) => {
    if (deleteEvent) deleteEvent(id);
  };

  const handlerSelect = (id: number) => {
    if (selectEvent) selectEvent(id);
  };
  return (
    <>
      <p>Lista de Grupos</p>
      <ul>
        {items.length > 0 ? (
          items.map((item, i) => {
            return (
              <li key={i}>
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
                <Tasks groupId={item.id ?? 0} />
                <br />
              </li>
            );
          })
        ) : (
          <li>No hay datos para mostrar </li>
        )}
      </ul>
    </>
  );
}

export { GroupsList };
