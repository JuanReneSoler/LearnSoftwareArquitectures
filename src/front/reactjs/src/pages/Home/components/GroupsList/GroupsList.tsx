import { Pagination } from "../../../../components";
import { Group } from "../../../../services";
import { CustonLi } from "./CustomLi";

interface IProps {
  items: Array<Group>;
  selectEvent?: (id: number) => void;
  deleteEvent?: (id: number) => void;
  totalPages: number;
  currentPage: number;
  changePagination: (newPage: number) => void;
}

function GroupsList({
  items,
  deleteEvent,
  selectEvent,
  totalPages,
  currentPage,
  changePagination,
}: IProps) {
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
      <Pagination
        totalPages={totalPages}
        currentPage={currentPage}
        changeCurrentPage={(newPage) => changePagination(newPage)}
      />
    </>
  );
}

export { GroupsList };
