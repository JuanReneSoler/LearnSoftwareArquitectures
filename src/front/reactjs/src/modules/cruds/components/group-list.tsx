import { useEffect, useState } from "react";
import { Group } from "../dtos";
import { GroupService } from "../services";
import { TaskList } from "./task-list";

function GroupList() {
  const [list, setList] = useState(Array<Group>);

  useEffect(() => {
    (async () => {
      GroupService.List().then((res) => {
        setList(res);
      });
    })();
  }, []);

  return (
    <>
      <p>Lista de Grupos</p>
      <ul>
        {list.map((item, i) => {
          return (
            <li key={i}>
              {item.name}
              <a href="#">( ver )</a>
              <a href="#">( eliminar )</a>
              <a href="#">( ver tareas )</a>
              <TaskList groupId={item.id} />
            </li>
          );
        })}
      </ul>
    </>
  );
}

export { GroupList };
