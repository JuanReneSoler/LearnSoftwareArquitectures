import { useEffect, useState } from "react";
import { PeopleList, Person, PersonForm, PersonViewModel } from "../People";
import { peopleService } from "../../services";
import { Person as PersonDto } from "../../services";

const initialState = {
  id: 0,
  name: "",
} as PersonViewModel;

const transform = (group: PersonViewModel): PersonDto => {
  return {
    id: group.id,
    name: group.name,
  } as PersonDto;
};

function PeopleManagement() {
  const [personList, setPersonList] = useState([] as Array<Person>);
  const [showForm, setShowForm] = useState(false);
  const [personForm, setPersonForm] = useState(initialState);
  const formId = "group";

  useEffect(() => {
    (async () => {
      await loadPersonList();
    })();
  }, []);

  const loadPersonList = async () => {
    await peopleService.filter().then((res) => {
      setPersonList(res);
    });
  };

  const handlerSubmit = (result: PersonViewModel) => {
    (async () => {
      await peopleService.create(transform(result)).then(async () => {
        await loadPersonList();
        alert("grupo creado satisfactoriamente!");
      });
    })();
  };

  const handlerSelect = (id: number) => {
    const item = personList.filter((x) => x.id === id)[0];
    setPersonForm(transform(item));
  };

  const handlerDelete = (id: number) => {
    (async () => {
      await peopleService.delete(id).then(async () => {
        await loadPersonList();
      });
    })();
  };

  return (
    <>
      <br />
      <br />
      {!showForm && (
        <button type="button" onClick={() => setShowForm(true)}>
          crear persona
        </button>
      )}
      {showForm && (
        <button type="submit" form={formId}>
          guardar
        </button>
      )}
      {showForm && (
        <button type="button" onClick={() => setShowForm(false)}>
          cancelar
        </button>
      )}
      {showForm && (
        <PersonForm
          id={formId}
          viewModel={personForm}
          submitEvent={handlerSubmit}
        />
      )}
      <PeopleList
        items={personList}
        selectEvent={handlerSelect}
        deleteEvent={handlerDelete}
      />
    </>
  );
}

export default PeopleManagement;
