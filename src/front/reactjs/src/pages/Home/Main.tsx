import React, { useState } from "react";
import { TasksManagement, PeopleManagement, GroupsManagement } from './components';

function Home() {
  const [showTasks, setShowTasks] = useState(true);
  const [showGroups, setshowGroups] = useState(false);
  const [showPeople, setShowPeople] = useState(false);

  const viewTasks = () => {
    setShowTasks(true);
    setshowGroups(false);
    setShowPeople(false);
  };

  const viewGroups = () => {
    setShowTasks(false);
    setshowGroups(true);
    setShowPeople(false);
  };

  const viewPeople = () => {
    setShowTasks(false);
    setshowGroups(false);
    setShowPeople(true);
  };

  return (
    <>
      <button type="button" onClick={viewTasks}>
        Lista de Tareas {showTasks ? "*" : ""}
      </button>
      <button type="button" onClick={viewGroups}>
        Lista de Grupos {showGroups ? "*" : ""}
      </button>
      <button type="button" onClick={viewPeople}>
        Lista de personas {showPeople ? "*" : ""}
      </button>

      {showTasks && <TasksManagement />}
      {showGroups && <GroupsManagement />}
      {showPeople && <PeopleManagement />}
    </>
  );
}

export { Home };
