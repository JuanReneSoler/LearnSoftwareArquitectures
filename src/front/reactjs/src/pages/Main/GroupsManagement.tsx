import { useEffect, useState } from "react";
import { Group, GroupForm, GroupViewModel, GroupsList } from "../Groups";
import { groupService } from "../../services";

function GroupsManagement() {
  const [groupList, setGroupList] = useState([] as Array<Group>);
  const [showForm, setShowForm] = useState(false);
  useEffect(() => {
    (async () => {
      await groupService.filter().then((res) => {
        setGroupList(res);
      });
    })();
  }, []);
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
        <button type="button" onClick={() => setShowForm(false)}>
          cancelar
        </button>
      )}
      {showForm && <GroupForm id="" viewmodel={{} as GroupViewModel} />}
      <GroupsList items={groupList} />
    </>
  );
}

export default GroupsManagement;
