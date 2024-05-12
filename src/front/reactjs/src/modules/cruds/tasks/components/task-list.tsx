import { useContext, useEffect, useState } from "react";
import { TaskContext } from "../contexts";
import { TaskService } from "../services";
import { Task } from "../dtos";

const TaskList = () => {
  const [tasks, setTaks] = useState([] as Array<Task>);
  const { shareSelectedTask } = useContext(TaskContext);

  useEffect(() => {
    (async () => {
      await TaskService.List().then((res) => {
        setTaks(res);
      });
    })();
  }, []);

  const eliminar = (id: number) => {
    if (confirm("esta segudo de eliminar esta tarea?")) {
      (async () => {
        await TaskService.Delete(id).then(() => {
          shareSelectedTask(null);
        });
      })();
    }
  };

  return (
    <>
      <p>Lista de Tareas</p>
      <ul>
        {tasks.length > 0 ? (
          tasks.map((task, i) => {
            return (
              <li key={i}>
                {task.title}
                <a href="#" onClick={() => shareSelectedTask(task)}>
                  ( ver )
                </a>
                <a href="#" onClick={() => eliminar(task.id)}>
                  ( eliminar )
                </a>
              </li>
            );
          })
        ) : (
          <li key={0}>no hay elementos para mostrar</li>
        )}
      </ul>
    </>
  );
};
export { TaskList };
