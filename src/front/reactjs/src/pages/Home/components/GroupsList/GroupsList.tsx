import { CustonLi } from "./CustomLi";

export interface IGroup {
  id: number;
  name: string;
}

interface IProps {
  items: Array<IGroup>;
  selectEvent?: (id: number) => void;
  deleteEvent?: (id: number) => void;
}

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
              <CustonLi
                key={i}
                item={item}
                handlerDelete={handlerDelete}
                handlerSelect={handlerSelect}
              />
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
