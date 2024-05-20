import { taskService } from "../../../../services";
import { ITask, TasksList } from "..";
import { useEffect, useState } from "react";

interface IProps {
  groupId?: number;
}

export const Tasks = ({ groupId }: IProps) => {
  const [items, setItems] = useState([] as Array<ITask>);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(0);

  const loadDataList = async () => {
    await taskService
      .filter({ GroupId: groupId, page: currentPage, size: 10 })
      .then((res) => {
        setItems(res.items);
        setTotalPages(res.totalPages);
      });
  };

  useEffect(() => {
    (async () => {
      await loadDataList();
    })();
  }, [currentPage]);

  const handlerDropEvent = (task: ITask) => {
    (async () => {
      await taskService.changeGroup(task.id, groupId ?? 0).then(async () => {
        await loadDataList();
      });
    })();
  };

  return (
    <TasksList
      totalPages={totalPages}
      currentPage={currentPage}
      changePagination={(page) => setCurrentPage(page)}
      draggable={true}
      onDrop={handlerDropEvent}
      readonly={true}
      items={items}
    />
  );
};
