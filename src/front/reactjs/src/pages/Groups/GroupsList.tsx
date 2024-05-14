import { taskService } from "../../services";
import { Task, TasksList } from "../Tasks";
import { useEffect, useState } from "react";

export interface Group {
  id: number;
  name: string;
}

interface IProps {
  items: Array<Group>;
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

  return <TasksList items={items} />;
};

function GroupsList({ items }: IProps) {
  return (
    <>
      <p>Lista de Grupos</p>
      <ul>
        {items.length > 0 ? (
          items.map((item, i) => {
            return (
              <li key={i}>
                {item.name}-<a href="">( ver )</a>-<a href="">( eliminar )</a>
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
