import { ChangeEvent, FormEvent, useEffect, useState } from "react";

export interface GroupViewModel {
  id: number;
  name: string;
}

interface IProps {
  id: string;
  viewModel: GroupViewModel;
  submitEvent: (viewModel: GroupViewModel) => void;
}

function GroupForm({ id, viewModel, submitEvent }: IProps) {
  const [vModel, setViewModel] = useState(viewModel);

  const handlerSubmit = async (e: FormEvent) => {
    e.preventDefault();
    submitEvent(vModel);
    setViewModel(viewModel);
  };

  const handlerChange = (e: ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setViewModel({ ...vModel, [name]: value });
  };

  useEffect(() => {
    setViewModel(viewModel);
  }, [viewModel]);

  return (
    <form id={id} onSubmit={handlerSubmit}>
      <p>Grupo</p>
      <input
        type="hidden"
        name="id"
        onChange={handlerChange}
        value={vModel.id}
      />
      <div>
        <label htmlFor="">Nombre:</label>
        <input
          type="text"
          name="name"
          onChange={handlerChange}
          value={vModel.name}
          id=""
        />
      </div>
    </form>
  );
}

export { GroupForm };
