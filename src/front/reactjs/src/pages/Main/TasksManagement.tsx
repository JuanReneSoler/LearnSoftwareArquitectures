import { useEffect, useState } from "react";
import { Task, TaskForm, TaskFormViewModel, TasksList } from "../Tasks";
import { taskService } from "../../services";
import { Task as TaskDto } from "./../../services";

const formInitialState = {
  id: 0,
  title: "",
  description: "",
  groupId: 0,
  personId: 0,
} as TaskFormViewModel;

const transform = (task: TaskFormViewModel) => {
  return {
    id: task.id,
    title: task.title,
    description: task.description,
    groupId: task.groupId,
    personId: task.personId,
  } as TaskDto;
};

const TasksManagement = () => {
  const [showForm, setShowForm] = useState(false);
  const [taskList, setTaskList] = useState([] as Array<Task>);
  const [taskForm, setTaskForm] = useState(formInitialState);
  const [isReadOnly, setIsReadOnly] = useState(true);
  const formId = "task";

  const loadTaskList = async () => {
    await taskService.filter().then((res) => {
      setTaskList(
        res.map((i) => {
          return {
            id: i.id,
            title: i.title,
            description: i.description,
            groupId: i.groupId,
            groupName: i.group.name,
            personId: i.personId,
            personName: i.person.name,
          };
        })
      );
    });
  };

  useEffect(() => {
    (async () => {
      await loadTaskList();
    })();
  }, []);

  const handlerSubmit = (result: TaskFormViewModel) => {
    (async () => {
      if (result.id > 0) {
        await taskService.update(transform(result)).then(async () => {
          await loadTaskList();
          alert("tarea creada satisfactoriamente!");
        });
      } else {
        await taskService.create(transform(result)).then(async () => {
          await loadTaskList();
          alert("tarea creada satisfactoriamente!");
        });
      }
    })();
  };

  const handlerSelect = (id: number) => {
    const vm = taskList.filter((x) => x.id === id)[0];
    setTaskForm(
      transform({
        id: vm.id,
        title: vm.title,
        description: vm.description,
        groupId: vm.groupId,
        personId: vm.personId,
      })
    );
    setShowForm(true);
    setIsReadOnly(true);
  };

  const handlerDelete = (id: number) => {
    (async () => {
      await taskService.delete(id).then(async () => {
        await loadTaskList();
      });
    })();
  };

  return (
    <>
      <br />
      <br />
      {!showForm && (
        <button
          type="button"
          onClick={() => {
            setShowForm(true);
            setIsReadOnly(false);
            setTaskForm(formInitialState);
          }}
        >
          crear nueva tarea
        </button>
      )}
      {showForm && !isReadOnly && (
        <button type="submit" form={formId}>
          guardar
        </button>
      )}
      {isReadOnly && showForm && (
        <button type="button" onClick={() => setIsReadOnly(false)}>
          editar
        </button>
      )}
      {showForm && (
        <button
          type="button"
          onClick={() => {
            setShowForm(false);
            setIsReadOnly(true);
          }}
        >
          cancelar
        </button>
      )}
      {showForm && (
        <TaskForm
          id={formId}
          submitEvent={handlerSubmit}
          viewModel={taskForm}
          readonly={isReadOnly}
        />
      )}
      <TasksList
        items={taskList}
        selectEvent={handlerSelect}
        deleteEvent={handlerDelete}
      />
    </>
  );
};

export { TasksManagement };
