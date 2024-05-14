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
}
function TasksList({ items, deleteEvent, selectEvent, readonly }: IProps) {
  const handlerDelete = (id: number) => {
    if (deleteEvent) deleteEvent(id);
  };

  const handlerSelect = (id: number) => {
    if (selectEvent) selectEvent(id);
  };

  return (
    <>
      <p>Lita de Tareas</p>
      <ul>
        {items.length > 0 ? (
          items.map((item, i) => (
            <li key={i}>
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
