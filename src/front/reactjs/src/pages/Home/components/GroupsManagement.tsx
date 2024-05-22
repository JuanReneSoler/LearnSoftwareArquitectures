import { useContext, useEffect, useState } from "react";
import { IGroup, GroupForm, IGroupViewModel, GroupsList } from ".";
import { Group as GroupDto, groupService } from "../../../services";
import { AppContext } from "../../../contexts";

const initialState = {
  id: 0,
  name: "",
} as IGroupViewModel;

const transform = (group: IGroupViewModel): GroupDto => {
  return {
    id: group.id,
    name: group.name,
  } as GroupDto;
};

function GroupsManagement() {
  const [groupList, setGroupList] = useState([] as Array<IGroup>);
  const [showForm, setShowForm] = useState(false);
  const [groupForm, setGroupForm] = useState(initialState);
  const [isReadOnly, setIsReadOnly] = useState(true);
  const formId = "group";
  const [totalPages, setTotalPages] = useState(0);
  const [currentPage, setCurrentPage] = useState(1);
  const {globalSearchText}=useContext(AppContext);

  useEffect(() => {
    (async () => {
      await loadGroupList();
    })();
  }, [currentPage, globalSearchText]);

  const loadGroupList = async () => {
    await groupService.filter({ search:globalSearchText, page: currentPage, size: 10 }).then((res) => {
      setCurrentPage(res.currentPage);
      setTotalPages(res.totalPages);
      setGroupList(res.items);
    });
  };

  const handlerSubmit = (result: IGroupViewModel) => {
    (async () => {
      if (result.id > 0) {
        await groupService.update(transform(result)).then(async () => {
          await loadGroupList();
          alert("grupo modificado satisfactoriamente!");
        });
      } else {
        await groupService.create(transform(result)).then(async () => {
          await loadGroupList();
          alert("grupo creado satisfactoriamente!");
        });
      }
    })();
  };

  const handlerSelect = (id: number) => {
    const item = groupList.filter((x) => x.id === id)[0];
    setGroupForm(transform(item));
    setShowForm(true);
    setIsReadOnly(true);
  };

  const handlerDelete = (id: number) => {
    (async () => {
      await groupService.delete(id).then(async () => {
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
        >
          crear nuevo grupo
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
        <GroupForm
          id={formId}
          viewModel={groupForm}
          submitEvent={handlerSubmit}
          readonly={isReadOnly}
        />
      )}
      <GroupsList
        currentPage={currentPage}
        totalPages={totalPages}
        items={groupList}
        selectEvent={handlerSelect}
        deleteEvent={handlerDelete}
        changePagination={(page) => setCurrentPage(page)}
      />
    </>
  );
}

export { GroupsManagement };
