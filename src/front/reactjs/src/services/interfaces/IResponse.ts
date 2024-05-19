export interface IResponse<T> {
  items: Array<T>;
  totalPages: number;
  currentPage: number;
}
