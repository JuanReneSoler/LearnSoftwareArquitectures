import { useState } from "react";
import {
  TasksManagement,
  PeopleManagement,
  GroupsManagement,
} from "./components";
import { Searcher } from "../../components/searcher";

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
      <Searcher />
      <br />
      <a href="#" type="button" onClick={viewTasks}>
        (Lista de Tareas {showTasks ? "*" : ""})
      </a>
      <br />
      <a href="#" type="button" onClick={viewGroups}>
        (Lista de Grupos {showGroups ? "*" : ""})
      </a>
      <br />
      <a href="#" onClick={viewPeople}>
        (Lista de personas {showPeople ? "*" : ""})
      </a>

      {(showTasks) && <TasksManagement />}
      {showGroups && <GroupsManagement />}
      {showPeople && <PeopleManagement />}
    </>
  );
}

export { Home };
