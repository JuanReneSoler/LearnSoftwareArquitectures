import { useContext, useEffect, useState } from "react";
import { GroupForm, GroupsList } from ".";
import { Group as GroupDto, groupService } from "../../../services";
import { AppContext } from "../../../contexts";

const initialState = {
  id: 0,
  name: "",
} as GroupDto;

function GroupsManagement() {
  const [groupList, setGroupList] = useState([] as Array<GroupDto>);
  const [showForm, setShowForm] = useState(false);
  const [groupForm, setGroupForm] = useState(initialState);
  const [isReadOnly, setIsReadOnly] = useState(true);
  const formId = "group";
  const [totalPages, setTotalPages] = useState(0);
  const [currentPage, setCurrentPage] = useState(1);
  const { globalSearchText, token } = useContext(AppContext);

  useEffect(() => {
    (async () => {
      await loadGroupList();
    })();
  }, [currentPage, globalSearchText]);

  const loadGroupList = async () => {
    await groupService
      .filter({ search: globalSearchText, page: currentPage, size: 10 }, token)
      .then((res) => {
        setCurrentPage(res.currentPage);
        setTotalPages(res.totalPages);
        setGroupList(res.items);
      });
  };

  const handlerSubmit = (result: GroupDto) => {
    (async () => {
      if (result.id > 0) {
        await groupService.update(result, token).then(async () => {
          await loadGroupList();
          alert("grupo modificado satisfactoriamente!");
        });
      } else {
        await groupService.create(result, token).then(async () => {
          await loadGroupList();
          alert("grupo creado satisfactoriamente!");
        });
      }
    })();
  };

  const handlerSelect = (id: number) => {
    const item = groupList.filter((x) => x.id === id)[0];
    setGroupForm(item);
    setShowForm(true);
    setIsReadOnly(true);
  };

  const handlerDelete = (id: number) => {
    (async () => {
      await groupService.delete(id, token).then(async () => {
        await loadGroupList();
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
            setGroupForm(initialState);
          }}
          title="Crear Nuevo Grupo."
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
        <GroupForm
          id={formId}
          viewModel={groupForm}
          submitEvent={handlerSubmit}
          readonly={isReadOnly}
        />
      )}
      {!showForm && (
        <GroupsList
          currentPage={currentPage}
          totalPages={totalPages}
          items={groupList}
          selectEvent={handlerSelect}
          deleteEvent={handlerDelete}
          changePagination={(page) => setCurrentPage(page)}
        />
      )}
    </>
  );
}

export { GroupsManagement };
