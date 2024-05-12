import { useContext, useEffect, useState } from "react";
import { SesionContext } from "../modules/auth";
import { Form, PeopleList, TaskContext, TaskList } from "../modules/cruds";
import { GroupList } from "../modules/cruds/groups/components";

function Main() {
  const [showForm, setShowForm] = useState(false);
  const { logOut } = useContext(SesionContext);
  const { selectedTask, shareSelectedTask } = useContext(TaskContext);
  const [isReadOnly, setIsReadOnly] = useState(true);
  const formName = "formTask";

  useEffect(() => {
    setShowForm(selectedTask !== null && selectedTask.id > 0);
    setIsReadOnly(true);
  }, [selectedTask]);

  return (
    <>
      <button type="button" onClick={logOut}>
        loguot
      </button>

      {showForm && (
        <button
          onClick={() => {
            setShowForm(false);
            setIsReadOnly(true);
            shareSelectedTask(null);
          }}
          type="button"
        >
          Cancelar
        </button>
      )}

      {!showForm && (
        <button
          onClick={() => {
            setShowForm(true);
            setIsReadOnly(false);
            shareSelectedTask(null);
          }}
          type="button"
        >
          Crear Nueva Tarea
        </button>
      )}

      {isReadOnly && showForm && (
        <button
          type="button"
          onClick={() => {
            setIsReadOnly(!isReadOnly);
          }}
        >
          Editar
        </button>
      )}

      {!isReadOnly && showForm && <button form={formName}>Guardar</button>}

      <TaskList />
      <PeopleList />
      <GroupList />

      {showForm && (
        <Form
          id={formName}
          readonly={isReadOnly}
          onClose={() => {
            //
          }}
        />
      )}
    </>
  );
}

export { Main };
