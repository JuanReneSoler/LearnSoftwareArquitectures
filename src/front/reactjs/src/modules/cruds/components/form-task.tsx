import { ChangeEvent, FormEvent, useContext, useEffect, useState } from "react";
import { TaskContext } from "../contexts";
import { Group, Person, Task } from "../dtos";
import { GroupService, PersonService, TaskService } from "../services";

interface IProps {
  onClose?: () => void;
  readonly?: boolean;
  id: string;
}

const FormTask = ({ onClose, readonly = false, id }: IProps) => {
  const [grupos, setGrupos] = useState([] as Array<Group>);
  const [persons, setPersons] = useState([] as Array<Person>);
  const { selectedTask } = useContext(TaskContext);
  const [viewState, setViewState] = useState({
    description: "",
    title: "",
    id: 0,
    personId: 0,
    groupId: 0,
  } as Task);

  const handleChange = (
    e: ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) => {
    const { name, value } = e.target;
    setViewState({
      ...viewState,
      [name]: value,
    });
  };

  const handleSubmit = async (e: FormEvent<HTMLFormElement> | undefined) => {
    e?.preventDefault();

    if (viewState.id === 0) {
      await TaskService.Add(viewState).then(() => {
        alert("tarea creada datisfactoriamente.");
        if (onClose) onClose();
      });
    } else {
      await TaskService.Update(viewState).then(() => {
        alert("tarea actualizada datisfactoriamente.");
        if (onClose) onClose();
      });
    }
  };

  useEffect(() => {
    if (selectedTask) setViewState(selectedTask);
  }, [selectedTask]);

  useEffect(() => {
    (async () => {
      await GroupService.List().then((res) => {
        setGrupos(res);
      });
      await PersonService.List().then((res) => {
        setPersons(res);
      });
    })();
  }, []);

  return (
    <form id={id} onSubmit={handleSubmit}>
      <p>Tarea</p>
      <input type="hidden" value={viewState.id} name="id" />
      <div>
        <label htmlFor="">Titulo</label>
        <input
          onChange={handleChange}
          type="text"
          placeholder="Titulo de la tarea"
          name="title"
          required
          value={viewState.title}
          disabled={readonly}
        />
      </div>
      <div>
        <label htmlFor="">Descripción</label>
        <input
          onChange={handleChange}
          type="text"
          placeholder="Descripción de la tarea"
          name="description"
          required
          value={viewState.description}
          disabled={readonly}
        />
      </div>
      <div>
        <label htmlFor="">A quien Pertenece</label>
        <select
          name="personId"
          onChange={handleChange}
          required
          value={viewState.personId}
          disabled={readonly}
        >
          <option value="0" disabled>
            selecione una persona
          </option>
          {persons.map((person, i) => {
            return (
              <option key={i} value={person.id}>
                {person.name}
              </option>
            );
          })}
        </select>
      </div>
      <div>
        <label htmlFor="">Grupo</label>
        <select
          name="groupId"
          onChange={handleChange}
          required
          value={viewState.groupId}
          disabled={readonly}
        >
          <option value="0" disabled>
            selecione un grupo
          </option>
          {grupos.map((grupo, i) => {
            return (
              <option key={i} value={grupo.id}>
                {grupo.name}
              </option>
            );
          })}
        </select>
      </div>
    </form>
  );
};
export { FormTask };
