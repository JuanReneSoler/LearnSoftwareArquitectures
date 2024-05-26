import { useContext, useEffect, useState } from "react";
import { PeopleList, PersonForm } from ".";
import { Person as PersonDto, peopleService } from "../../../services";
import { AppContext } from "../../../contexts";

const initialState = {
  id: 0,
  name: "",
} as PersonDto;

function PeopleManagement() {
  const [personList, setPersonList] = useState([] as Array<PersonDto>);
  const [showForm, setShowForm] = useState(false);
  const [personForm, setPersonForm] = useState(initialState);
  const [isReadOnly, setIsReadOnly] = useState(true);
  const formId = "group";
  const [totalPages, setTotalPages] = useState(0);
  const [currentPage, setCurrentPage] = useState(1);
  const { globalSearchText, token } = useContext(AppContext);

  useEffect(() => {
    (async () => {
      await loadPersonList();
    })();
  }, [currentPage, globalSearchText]);

  const loadPersonList = async () => {
    await peopleService
      .filter({ search: globalSearchText, page: currentPage, size: 10 }, token)
      .then((res) => {
        setCurrentPage(res.currentPage);
        setTotalPages(res.totalPages);
        setPersonList(res.items);
      });
  };

  const handlerSubmit = (result: PersonDto) => {
    (async () => {
      if (result.id > 0) {
        await peopleService.update(result, token).then(() => {
          alert("persona modificada satisfactoriamente!");
        });
      } else {
        await peopleService.create(result, token).then(() => {
          alert("persona creada satisfactoriamente!");
        });
      }
      await loadPersonList();
    })();
  };

  const handlerSelect = (id: number) => {
    const item = personList.filter((x) => x.id === id)[0];
    setPersonForm(item);
    setShowForm(true);
    setIsReadOnly(true);
  };

  const handlerDelete = (id: number) => {
    (async () => {
      await peopleService.delete(id, token).then(async () => {
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
          title="Crear nueva persona."
        >
          crear
        </button>
      )}
      {showForm && !isReadOnly && (
        <button type="submit" form={formId} title="Guardar los datos.">
          guardar
        </button>
      )}
      {isReadOnly && showForm && (
        <button
          type="button"
          onClick={() => setIsReadOnly(false)}
          title="Guardar los cambios."
        >
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
          title="Cerrar el formulario y deshacer los cambios."
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
      {!showForm && (
        <PeopleList
          currentPage={currentPage}
          totalPages={totalPages}
          items={personList}
          selectEvent={handlerSelect}
          deleteEvent={handlerDelete}
          changePagination={(page) => setCurrentPage(page)}
        />
      )}
    </>
  );
}

export { PeopleManagement };
