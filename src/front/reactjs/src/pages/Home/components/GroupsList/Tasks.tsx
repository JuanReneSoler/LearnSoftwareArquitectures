import { Task, taskService } from "../../../../services";
import { TasksList } from "..";
import { useEffect, useState } from "react";

interface IProps {
  groupId?: number;
}

export const Tasks = ({ groupId }: IProps) => {
  const [items, setItems] = useState([] as Array<Task>);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(0);

  const loadDataList = async () => {
    await taskService
      .filter({ GroupId: groupId, page: currentPage, size: 10 })
      .then((res) => {
        setItems(res.items);
        setTotalPages(res.totalPages);
        setCurrentPage(res.currentPage);
      });
  };

  useEffect(() => {
    (async () => {
      await loadDataList();
    })();
  }, [currentPage]);

  const handlerDropEvent = (task: Task) => {
    (async () => {
      await taskService.changeGroup(task.id, groupId ?? 0).then(() => {
        //
      });
      await loadDataList();
    })();
  };

  const handlerDragEvent = () => {
    (async () => {
      await loadDataList();
    })();
  };

  return (
    <TasksList
      totalPages={totalPages}
      currentPage={currentPage}
      changePagination={(page) => setCurrentPage(page)}
      draggable={true}
      onDrop={handlerDropEvent}
      onDrag={handlerDragEvent}
      readonly={true}
      items={items}
    />
  );
};
