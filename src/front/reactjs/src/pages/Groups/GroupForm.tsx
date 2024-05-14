import { useState } from "react";

export interface GroupViewModel {
  id: number;
  name: string;
}

interface IProps {
  id: string;
  viewmodel: GroupViewModel;
}

function GroupForm({ id, viewmodel }: IProps) {
  const [vModel, setViewModel] = useState(viewmodel);

  return (
    <form id={id}>
      <p>Grupo</p>
      <input type="hidden" name="id" value={vModel.id} />
      <div>
        <label htmlFor="">Nombre:</label>
        <input type="text" name="name" value={vModel.name} id="" />
      </div>
    </form>
  );
}

export { GroupForm };
