import { useEffect, useState } from "react";
import { ITask, TasksList } from ".";
import { taskService } from "../../../services";

export interface IPerson {
  id: number;
  name: string;
}

interface ITasksProps {
  personId: number;
}

const Tasks = ({ personId }: ITasksProps) => {
  const [items, setItems] = useState([] as Array<ITask>);
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
  items: Array<IPerson>;
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
