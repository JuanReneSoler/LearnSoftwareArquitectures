import { useState } from "react";
import { Form, TaskList } from "./partials";

function Home() {
  const [showForm, setShowForm] = useState(false);

  const createTarea = () => {
    setShowForm(!showForm);
  };

  return (
    <>
      <button onClick={createTarea} type="button">
        {!showForm ? "Crear Nueva Tarea" : "Cancelar"}
      </button>
      {showForm ? <button form="formTarea">Guardar</button> : <></>}
      {!showForm ? <TaskList /> : <Form onClose={createTarea} />}
    </>
  );
}

export { Home };
