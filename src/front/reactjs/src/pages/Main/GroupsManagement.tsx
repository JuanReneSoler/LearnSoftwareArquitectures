import { useEffect, useState } from "react";
import { Group, GroupForm, GroupViewModel, GroupsList } from "../Groups";
import { groupService } from "../../services";
import { Group as GroupDto } from "../../services";

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
      await groupService.create(transform(result)).then(async () => {
        await loadGroupList();
        alert("grupo creado satisfactoriamente!");
      });
    })();
  };

  const handlerSelect = (id: number) => {
    const item = groupList.filter((x) => x.id === id)[0];
    setGroupForm(transform(item));
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
        <button type="button" onClick={() => setShowForm(true)}>
          crear nuevo grupo
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
        <GroupForm
          id={formId}
          viewModel={groupForm}
          submitEvent={handlerSubmit}
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

export default GroupsManagement;
