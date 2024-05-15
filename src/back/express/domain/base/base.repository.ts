import { BaseEntityDomain } from "./base.entity.domain";

export interface IBaseRepository<TEntity extends BaseEntityDomain<TEntityID>, TEntityID>
{
    Add(Entity:TEntity, cancelationToken:AbortSignal):Promise<null>;
    Update(entity:TEntity, id:TEntityID, cancelationToken:AbortSignal):Promise<null>;
    Delete(id:TEntityID, cancelationToken:AbortSignal):Promise<null>;
    Commit(cancelationToken:AbortSignal):Promise<boolean>;
    RollBack(cancelationToken:AbortSignal):Promise<null>;
}