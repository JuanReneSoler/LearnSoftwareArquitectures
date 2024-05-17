import { DragEvent, useEffect, useState } from "react";

export interface Task {
  id: number;
  title: string;
  description: string;
  groupId: number;
  personId: number;
}

interface IProps {
  items: Array<Task>;
  deleteEvent?: (id: number) => void;
  selectEvent?: (id: number) => void;
  readonly?: boolean;
  draggable?: boolean;
  onDrop?: (task: Task) => void;
}
function TasksList({
  items,
  deleteEvent,
  selectEvent,
  readonly,
  draggable,
  onDrop,
}: IProps) {
  const dragTask = "dragTask";

  const [tasks, setTasks] = useState([] as Array<Task>);

  useEffect(() => {
    setTasks(items);
  }, [items]);

  const handlerDelete = (id: number) => {
    if (deleteEvent) deleteEvent(id);
  };

  const handlerSelect = (id: number) => {
    if (selectEvent) selectEvent(id);
  };

  const handleDragStart = (e: DragEvent<HTMLLIElement>, tagId: number) => {
    const item = tasks.filter((x) => x.id === tagId)[0];
    e.dataTransfer.setData(dragTask, JSON.stringify(item));
  };

  const handleDragEndCapture = (e: DragEvent<HTMLLIElement>, tagId: number) => {
    e.preventDefault();
    const result = tasks.filter((x) => x.id !== tagId);
    setTasks([...result]);
  };

  const handleDragOver = (e: DragEvent<HTMLUListElement>) => {
    e.preventDefault();
  };

  const handleDrop = (e: DragEvent<HTMLUListElement>) => {
    e.preventDefault();
    const item = JSON.parse(e.dataTransfer.getData(dragTask)) as Task;

    if (tasks.filter((x) => x.id === item.id).length === 0) {
      if (onDrop) onDrop(item);
    }
  };

  return (
    <>
      <p>Lita de Tareas</p>
      <ul onDragOver={handleDragOver} onDrop={handleDrop}>
        {tasks.length > 0 ? (
          tasks.map((item, i) => (
            <li
              key={i}
              draggable={draggable}
              onDragStart={(e) => handleDragStart(e, item.id)}
              onDragEndCapture={(e) => handleDragEndCapture(e, item.id)}
            >
              {item.title}
              {!readonly && (
                <>
                  -
                  <a href="#" onClick={() => handlerSelect(item.id)}>
                    (ver)
                  </a>
                  -
                  <a href="#" onClick={() => handlerDelete(item.id)}>
                    (eliminar)
                  </a>
                </>
              )}
            </li>
          ))
        ) : (
          <li>No hay elementos para mostrar </li>
        )}
      </ul>
    </>
  );
}

export { TasksList };
