import { useContext, useEffect, useState } from "react";
import { TaskForm, TasksList } from ".";
import { Task as TaskDto, taskService } from "../../../services";
import { AppContext } from "../../../contexts";

const formInitialState = {
  id: 0,
  title: "",
  description: "",
  groupId: 0,
  personId: 0,
} as TaskDto;

const TasksManagement = () => {
  const [showForm, setShowForm] = useState(false);
  const [taskList, setTaskList] = useState([] as Array<TaskDto>);
  const [taskForm, setTaskForm] = useState(formInitialState);
  const [isReadOnly, setIsReadOnly] = useState(true);
  const formId = "task";
  const [totalPages, setTotalPages] = useState(0);
  const [currentPage, setCurrentPage] = useState(1);
  const { globalSearchText, token } = useContext(AppContext);

  const loadTaskList = async () => {
    await taskService
      .filter({ search: globalSearchText, page: currentPage, size: 10 }, token)
      .then((res) => {
        setCurrentPage(res.currentPage);
        setTotalPages(res.totalPages);
        setTaskList(res.items);
      });
  };

  useEffect(() => {
    (async () => {
      await loadTaskList();
    })();
  }, [currentPage, globalSearchText]);

  const handlerSubmit = (result: TaskDto) => {
    (async () => {
      if (result.id > 0) {
        await taskService.update(result, token).then(() => {
          alert("tarea modificada satisfactoriamente!");
        });
      } else {
        await taskService.create(result, token).then(() => {
          alert("tarea creada satisfactoriamente!");
        });
      }
      await loadTaskList();
    })();
  };

  const handlerSelect = (id: number) => {
    const vm = taskList.filter((x) => x.id === id)[0];
    setTaskForm(vm);
    setShowForm(true);
    setIsReadOnly(true);
  };

  const handlerDelete = (id: number) => {
    (async () => {
      await taskService.delete(id, token).then(async () => {
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
          title="Crear Nueva Tareas."
        >
          crear
        </button>
      )}
      {showForm && !isReadOnly && (
        <button type="submit" form={formId} title="Guardar los datos.">
          guardar
        </button>
      )}
      {isReadOnly && showForm && (
        <button
          type="button"
          onClick={() => setIsReadOnly(false)}
          title="Guardar los cambios."
        >
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
          title="Cerrar el formulario y deshacer todo."
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
      {!showForm && (
        <TasksList
          currentPage={currentPage}
          totalPages={totalPages}
          items={taskList}
          selectEvent={handlerSelect}
          deleteEvent={handlerDelete}
          changePagination={(page) => setCurrentPage(page)}
        />
      )}
    </>
  );
};

export { TasksManagement };
