import { useEffect, useState } from "react";
import { ITask, TasksList } from "..";
import { taskService } from "../../../../services";

interface IProps {
  personId: number;
}

export const Tasks = ({ personId }: IProps) => {
  const [items, setItems] = useState([] as Array<ITask>);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(0);

  useEffect(() => {
    (async () => {
      await taskService
        .filter({ PersonId: personId, page: currentPage, size: 10 })
        .then((res) => {
          setTotalPages(res.totalPages);
          setItems(res.items);
        });
    })();
  }, [currentPage]);

  return (
    <TasksList
      totalPages={totalPages}
      currentPage={currentPage}
      changePagination={(page) => setCurrentPage(page)}
      items={items}
      readonly={true}
    />
  );
};
