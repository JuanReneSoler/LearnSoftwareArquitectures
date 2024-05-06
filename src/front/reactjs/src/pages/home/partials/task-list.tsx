import { useEffect, useState } from "react";
import { Task, TaskService } from "../../../services";

const TaskList = () => {
  const [tasks, setTaks] = useState([] as Array<Task>);

  useEffect(() => {
    (async () => {
      await TaskService.List().then((res) => {
        setTaks(res);
      });
    })();
  }, []);

  return (
    <ul>
      {tasks.length > 0 ? (
        tasks.map((task, i) => {
          return (
            <li key={i}>
              {task.title}
              <a href="#">(ver completa)</a>
              <a href="#">(editar)</a>
              <a href="#">(eliminar)</a>
            </li>
          );
        })
      ) : (
        <li key={0}>no hay elementos para mostrar</li>
      )}
    </ul>
  );
};
export { TaskList };
