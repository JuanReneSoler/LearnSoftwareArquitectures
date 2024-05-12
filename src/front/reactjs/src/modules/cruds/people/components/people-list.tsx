import { useEffect, useState } from "react";
import { PersonService } from "../services";
import { Person } from "../dtos";

function PeopleList() {
  const [list, setList] = useState(Array<Person>);

  useEffect(() => {
    (async () => {
      await PersonService.List().then((res) => {
        setList(res);
      });
    })();
  }, []);

  return (
    <>
      <p>Lista de Personas</p>
      <ul>
        {list.map((item, i) => {
          return (
            <li key={i}>
              {item.name}
              <a href="#">( ver )</a>
              <a href="#">( eliminar )</a>
            </li>
          );
        })}
      </ul>
    </>
  );
}

export { PeopleList };
