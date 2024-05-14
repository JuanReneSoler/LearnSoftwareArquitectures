import { Group, Person } from ".";

class Task {
  id: number = 0;
  title: string = "";
  description: string = "";
  groupId: number = 0;
  group: Group = new Group();
  personId: number = 0;
  person: Person = new Person();
}
export { Task };
