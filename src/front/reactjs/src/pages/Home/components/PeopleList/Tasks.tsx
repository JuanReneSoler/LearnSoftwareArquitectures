import { useEffect, useState } from "react";
import { ITask, TasksList } from "..";
import { taskService } from "../../../../services";

interface IProps {
  personId: number;
}

export const Tasks = ({ personId }: IProps) => {
  const [items, setItems] = useState([] as Array<ITask>);
  useEffect(() => {
    (async () => {
      await taskService.filter({ PersonId: personId }).then((res) => {
        setItems(res);
      });
    })();
  }, [personId]);

  return <TasksList items={items} readonly={true} />;
};
