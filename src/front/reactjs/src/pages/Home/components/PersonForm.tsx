import { ChangeEvent, FormEvent, useEffect, useState } from "react";
import { Person } from "../../../services";

interface IProps {
  id: string;
  viewModel: Person;
  submitEvent: (viewModel: Person) => void;
  readonly?: boolean;
}

function PersonForm({ id, viewModel, submitEvent, readonly }: IProps) {
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
      <p>Persona</p>
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
      <div>
        <label htmlFor="">Correo:</label>
        <input
          type="text"
          name="email"
          onChange={handlerChange}
          value={vModel.email}
          disabled={readonly}
        />
      </div>
    </form>
  );
}

export { PersonForm };
