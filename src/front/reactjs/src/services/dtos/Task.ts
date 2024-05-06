import { Group, Person } from ".";

class Task {
  id: number;
  title: string;
  description: string;
  groupId: number;
  group: Group;
  personId: number;
  person: Person;

  constructor() {
    this.id = 0;
    this.title = "";
    this.description = "";
    this.groupId = 0;
    this.group = new Group();
    this.personId = 0;
    this.person = new Person();
  }
}
export { Task };
