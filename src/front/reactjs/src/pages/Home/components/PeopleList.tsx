import { useEffect, useState } from "react";
import { Task, TasksList } from ".";
import { taskService } from "../../../services";

export interface Person {
  id: number;
  name: string;
}

interface TasksProps {
  personId: number;
}

const Tasks = ({ personId }: TasksProps) => {
  const [items, setItems] = useState([] as Array<Task>);
  useEffect(() => {
    (async () => {
      await taskService.filter({ PersonId: personId }).then((res) => {
        setItems(res);
      });
    })();
  }, [personId]);

  return <TasksList items={items} readonly={true} />;
};

interface IProps {
  items: Array<Person>;
  selectEvent?: (id: number) => void;
  deleteEvent?: (id: number) => void;
}

function PeopleList({ items, deleteEvent, selectEvent }: IProps) {
  const handlerDelete = (id: number) => {
    if (deleteEvent) deleteEvent(id);
  };

  const handlerSelect = (id: number) => {
    if (selectEvent) selectEvent(id);
  };
  return (
    <>
      <p>Lista de Personas</p>
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
                <Tasks personId={item.id} />
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

export { PeopleList };
