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
  const [isReadOnly, setIsReadOnly] = useState(true);
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
      if (result.id > 0) {
        await peopleService.update(transform(result)).then(async () => {
          await loadPersonList();
          alert("persona modificada satisfactoriamente!");
        });
      } else {
        await peopleService.create(transform(result)).then(async () => {
          await loadPersonList();
          alert("persona creada satisfactoriamente!");
        });
      }
    })();
  };

  const handlerSelect = (id: number) => {
    const item = personList.filter((x) => x.id === id)[0];
    setPersonForm(transform(item));
    setShowForm(true);
    setIsReadOnly(true);
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
        <button
          type="button"
          onClick={() => {
            setShowForm(true);
            setIsReadOnly(false);
            setPersonForm(initialState);
          }}
        >
          crear neeva persona
        </button>
      )}
      {showForm && !isReadOnly && (
        <button type="submit" form={formId}>
          guardar
        </button>
      )}
      {isReadOnly && showForm && (
        <button type="button" onClick={() => setIsReadOnly(false)}>
          editar
        </button>
      )}
      {showForm && (
        <button
          type="button"
          onClick={() => {
            setShowForm(false);
            setIsReadOnly(true);
          }}
        >
          cancelar
        </button>
      )}
      {showForm && (
        <PersonForm
          id={formId}
          viewModel={personForm}
          submitEvent={handlerSubmit}
          readonly={isReadOnly}
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
