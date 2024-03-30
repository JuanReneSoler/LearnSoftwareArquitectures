using Application.Base;

namespace Application.Services;

public interface IGenericService<TEntity> : IBaseService<TEntity, int>
    where TEntity : DtoBase<int>
{
}
