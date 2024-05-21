import { ChangeEvent, FormEvent, useEffect, useState } from "react";

export interface IGroupViewModel {
  id: number;
  name: string;
}

interface IProps {
  id: string;
  viewModel: IGroupViewModel;
  submitEvent: (viewModel: IGroupViewModel) => void;
  readonly?: boolean;
}

function GroupForm({ id, viewModel, submitEvent, readonly }: IProps) {
  const [vModel, setViewModel] = useState(viewModel);

  const handlerSubmit = async (e: FormEvent) => {
    e.preventDefault();
    submitEvent(vModel);
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
        disabled={readonly}
      />
      <div>
        <label htmlFor="">Nombre:</label>
        <input
          type="text"
          name="name"
          onChange={handlerChange}
          value={vModel.name}
          id=""
          disabled={readonly}
        />
      </div>
    </form>
  );
}

export { GroupForm };
