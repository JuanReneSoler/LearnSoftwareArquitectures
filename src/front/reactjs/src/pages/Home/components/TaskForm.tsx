import { ChangeEvent, FormEvent, useEffect, useState } from "react";
import { groupService, peopleService } from "../../../services";

export interface ITaskFormViewModel {
  id: number;
  title: string;
  description: string;
  groupId: number;
  personId: number;
}

interface IProps {
  viewModel: ITaskFormViewModel;
  id: string;
  submitEvent: (viewModel: ITaskFormViewModel) => void;
  readonly?: boolean;
}
function TaskForm({ viewModel, id, submitEvent, readonly }: IProps) {
  const [vModel, setViewModel] = useState(viewModel);
  const [groupList, setGroupList] = useState(
    [] as Array<{ id: number; name: string }>
  );
  const [peopleList, setPeopleList] = useState(
    [] as Array<{ id: number; name: string }>
  );

  useEffect(() => {
    (async () => {
      await groupService.filter({ page: 1, size: 10 }).then((res) => {
        setGroupList(
          res.items.map((item) => {
            return { id: item.id, name: item.name };
          })
        );
      });
      await peopleService.filter({ page: 1, size: 10 }).then((res) => {
        setPeopleList(
          res.items.map((item) => {
            return { id: item.id, name: item.name };
          })
        );
      });
    })();
  }, []);

  useEffect(() => {
    setViewModel(viewModel);
  }, [viewModel]);

  const handlerChange = (
    e: ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) => {
    const { name, value } = e.target;
    setViewModel({ ...vModel, [name]: value });
  };
  const handlerSubmit = async (e: FormEvent) => {
    e.preventDefault();
    submitEvent(vModel);
  };

  return (
    <form id={id} onSubmit={handlerSubmit}>
      <p>Tarea</p>
      <input type="hidden" name="id" value={vModel.id} />
      <div>
        <label htmlFor="">Title:</label>
        <input
          type="text"
          placeholder="Title"
          name="title"
          value={vModel.title}
          onChange={handlerChange}
          disabled={readonly}
        />
      </div>
      <div>
        <label htmlFor="">Descripción:</label>
        <input
          type="text"
          placeholder="Descripción"
          name="description"
          value={vModel.description}
          onChange={handlerChange}
          disabled={readonly}
        />
      </div>
      <div>
        <label htmlFor="">Propietario</label>
        <select
          name="personId"
          value={vModel.personId}
          onChange={handlerChange}
          //defaultValue={0}
          disabled={readonly}
        >
          <option value="0">Selecione una persona</option>
          {peopleList.map((item, i) => {
            return (
              <option key={i} value={item.id}>
                {item.name}
              </option>
            );
          })}
        </select>
      </div>
      <div>
        <label htmlFor="">Grupo</label>
        <select
          name="groupId"
          value={vModel.groupId}
          onChange={handlerChange}
          //defaultValue={0}
          disabled={readonly}
        >
          <option value="0">Selecione un grupo</option>
          {groupList.map((item, i) => {
            return (
              <option key={i} value={item.id}>
                {item.name}
              </option>
            );
          })}
        </select>
      </div>
    </form>
  );
}

export { TaskForm };
