import { useEffect, useState } from "react";
import { Group, GroupForm, GroupViewModel, GroupsList } from ".";
import { Group as GroupDto, groupService } from "../../../services";

const initialState = {
  id: 0,
  name: "",
} as GroupViewModel;

const transform = (group: GroupViewModel): GroupDto => {
  return {
    id: group.id,
    name: group.name,
  } as GroupDto;
};

function GroupsManagement() {
  const [groupList, setGroupList] = useState([] as Array<Group>);
  const [showForm, setShowForm] = useState(false);
  const [groupForm, setGroupForm] = useState(initialState);
  const [isReadOnly, setIsReadOnly] = useState(true);
  const formId = "group";

  useEffect(() => {
    (async () => {
      await loadGroupList();
    })();
  }, []);

  const loadGroupList = async () => {
    await groupService.filter().then((res) => {
      setGroupList(res);
    });
  };

  const handlerSubmit = (result: GroupViewModel) => {
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
        items={groupList}
        selectEvent={handlerSelect}
        deleteEvent={handlerDelete}
      />
    </>
  );
}

export { GroupsManagement };
