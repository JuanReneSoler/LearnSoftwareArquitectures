export interface Person {
  id: number;
  name: string;
}

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
