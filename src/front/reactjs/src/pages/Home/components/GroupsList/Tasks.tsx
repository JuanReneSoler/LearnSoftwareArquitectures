import { taskService } from "../../../../services";
import { ITask, TasksList } from "..";
import { useEffect, useState } from "react";

interface IProps {
  groupId?: number;
}

export const Tasks = ({ groupId }: IProps) => {
  const [items, setItems] = useState([] as Array<ITask>);

  const loadDataList = async (_groupId: number) => {
    await taskService
      .filter({ GroupId: _groupId })
      .then((res) => setItems(res));
  };

  useEffect(() => {
    (async () => {
      await loadDataList(groupId ?? 0);
    })();
  }, [groupId]);

  const handlerDropEvent = (task: ITask) => {
    (async () => {
      await taskService.changeGroup(task.id, groupId ?? 0).then(async () => {
        await loadDataList(groupId ?? 0);
      });
    })();
  };

  return (
    <TasksList
      draggable={true}
      onDrop={handlerDropEvent}
      readonly={true}
      items={items}
    />
  );
};
