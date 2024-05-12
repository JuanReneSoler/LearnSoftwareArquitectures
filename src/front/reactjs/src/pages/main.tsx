import { useContext, useEffect, useState } from "react";
import { SesionContext } from "../modules/auth";
import {
  FormGroup,
  FormPerson,
  FormTask,
  GroupList,
  PeopleList,
  TaskContext,
  TaskList,
} from "../modules/cruds";

function Main() {
  const [showFormTask, setShowFormTask] = useState(false);
  const [showFormGroup, setShowFormGroup] = useState(false);
  const [showFormPerson, setShowFormPerson] = useState(false);
  const { logOut } = useContext(SesionContext);
  const { selectedTask, shareSelectedTask } = useContext(TaskContext);
  const [isReadOnly, setIsReadOnly] = useState(true);
  const formName = "formTask";
  const [viewGroups, setViewGrups] = useState(false);
  const [viewTasks, setViewTasks] = useState(true);
  const [viewPeople, setViewPeople] = useState(false);

  useEffect(() => {
    setShowFormTask(selectedTask !== null && selectedTask.id > 0);
    setIsReadOnly(true);
  }, [selectedTask]);

  const viewTaskList = () => {
    setViewGrups(false);
    setViewTasks(true);
    setViewPeople(false);
    setIsReadOnly(true);
    setShowFormTask(false);
    setShowFormGroup(false);
    setShowFormPerson(false);
  };

  const viewGroupsList = () => {
    setViewGrups(true);
    setViewTasks(false);
    setViewPeople(false);
    setIsReadOnly(true);
    setShowFormTask(false);
    setShowFormGroup(false);
    setShowFormPerson(false);
  };

  const viewPeopleList = () => {
    setViewGrups(false);
    setViewTasks(false);
    setViewPeople(true);
    setIsReadOnly(true);
    setShowFormTask(false);
    setShowFormGroup(false);
    setShowFormPerson(false);
  };

  const editarTask = () => {
    setIsReadOnly(false);
    setShowFormTask(true);
  };

  const cancel = () => {
    setIsReadOnly(true);
    setShowFormTask(false);
    setShowFormGroup(false);
    setShowFormPerson(false);
  };

  const newTask = () => {
    setIsReadOnly(false);
    setShowFormTask(true);
    shareSelectedTask(null);
    setShowFormGroup(false);
    setShowFormPerson(false);
  };

  const newGroup = () => {
    setIsReadOnly(true);
    setShowFormGroup(true);
    setShowFormPerson(false);
    setShowFormTask(false);
  };

  const newPeople = () => {
    setIsReadOnly(true);
    setShowFormPerson(true);
    setShowFormGroup(false);
    setShowFormTask(false);
  };

  return (
    <>
      <button type="button" onClick={logOut}>
        loguot
      </button>

      {showFormTask && (
        <button type="button" onClick={cancel}>
          Cancelar
        </button>
      )}

      {viewTasks && !showFormTask && (
        <button type="button" onClick={newTask}>
          Crear nueva tarea
        </button>
      )}

      {viewGroups && (
        <button type="button" onClick={newGroup}>
          Crear nuevo grupo
        </button>
      )}
      {viewPeople && (
        <button type="button" onClick={newPeople}>
          Crear nueva persona
        </button>
      )}

      {isReadOnly && showFormTask && (
        <button type="button" onClick={editarTask}>
          Editar
        </button>
      )}

      {!isReadOnly && showFormTask && <button form={formName}>Guardar</button>}

      <div>
        <a href="#" onClick={viewTaskList}>
          Lista de Tareas {viewTasks ? "*" : ""}
        </a>
        <br />
        <a href="#" onClick={viewGroupsList}>
          Lista de Grupos {viewGroups ? "*" : ""}
        </a>
        <br />
        <a href="#" onClick={viewPeopleList}>
          Lista de Personas {viewPeople ? "*" : ""}
        </a>
      </div>

      {viewTasks && <TaskList />}
      {viewPeople && <PeopleList />}
      {viewGroups && <GroupList />}

      {showFormTask && (
        <FormTask
          id={formName}
          readonly={isReadOnly}
          onClose={() => {
            //
          }}
        />
      )}
      {showFormGroup && <FormGroup />}
      {showFormPerson && <FormPerson />}
    </>
  );
}

export { Main };
