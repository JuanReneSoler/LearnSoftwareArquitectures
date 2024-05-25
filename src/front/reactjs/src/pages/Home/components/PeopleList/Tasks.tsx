import { useEffect, useState } from "react";
import { TasksList } from "..";
import { Task, taskService } from "../../../../services";

interface IProps {
  personId: number;
}

export const Tasks = ({ personId }: IProps) => {
  const [items, setItems] = useState([] as Array<Task>);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(0);

  const loadTasks = (page: number) => {
    (async () => {
      await taskService
        .filter({ PersonId: personId, page: page, size: 10 })
        .then((res) => {
          setCurrentPage(res.currentPage);
          setTotalPages(res.totalPages);
          setItems(res.items);
        });
    })();
  };

  useEffect(() => {
    loadTasks(currentPage);
  }, []);

  return (
    <TasksList
      totalPages={totalPages}
      currentPage={currentPage}
      changePagination={(page) => loadTasks(page)}
      items={items}
      readonly={true}
    />
  );
};
