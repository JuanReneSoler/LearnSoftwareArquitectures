import { Group } from './Group';
import { Person } from './Person';

export class Task {
  id: number = 0;
  title: string = '';
  description: string = '';
  groupId: number = 0;
  group: Group = new Group();
  personId: number = 0;
  person: Person = new Person();
}
