import { Group, Person } from ".";

class Task {
  Id: number;
  Title: string;
  Description: string;
  GroupId: number;
  Group: Group;
  PersonId: number;
  Person: Person;

  constructor() {
    this.Id = 0;
    this.Title = "";
    this.Description = "";
    this.GroupId = 0;
    this.Group = new Group();
    this.PersonId = 0;
    this.Person = new Person();
  }
}
export { Task };
