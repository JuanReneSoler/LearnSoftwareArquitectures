using Domain.Entities;
using System.Linq.Expressions;
using EasyMapper;
using Application.UnitOfWorks;

namespace Application.UsesCases;

public interface IPersonUseCase : IGenericUseCase<PersonDto>
{
    Task<IBasePagination<PersonDto>> Filter(string Search, int page, int size, CancellationToken cancellationToken);
}

public sealed class PersonUseCase : IPersonUseCase
{
    private readonly IGenericUnitOfWork _uow;
    private readonly IMapper _mapper;

    public PersonUseCase(
            IGenericUnitOfWork UoW,
            IMapper Mapper)
    {
        _uow = UoW;
        _mapper = Mapper;
    }

    public async Task<PersonDto> Create(PersonDto Dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<PersonDto, Person>(Dto);
        await _uow.People.Add(entity, cancellationToken);
        if (await _uow.Commit(cancellationToken))
        {
            Dto.Id = entity.Id;
            return Dto;
        }
        {
            await _uow.Rollback(cancellationToken);
            throw new Exception("No fue posible crear este registro.");
        }
    }

    public async Task<int> Delete(int Id, CancellationToken cancellationToken)
    {
        if(await _uow.People.Exist(x=>x.Id == Id, cancellationToken))
        {
            await _uow.People.Delete(Id, cancellationToken);
            return await _uow.Commit(cancellationToken) ? Id : throw new Exception("No fue posible eliminar este registro.");
        }
        throw new NullReferenceException("Esta persona no existe.");
    }

    public async Task<IBasePagination<PersonDto>> Filter(string Search, int page, int size, CancellationToken cancellationToken)
    {
        Expression<Func<Person, bool>> expression = x => x.Id > 0;

        if (!string.IsNullOrEmpty(Search) && !string.IsNullOrWhiteSpace(Search))
        {
            expression = expression.And(x => x.Name.Contains(Search));
        }
        
        var query = await _uow.People.Where(expression, cancellationToken);
        return query.Select(x=>_mapper.Map<Person, PersonDto>(x)).Paginate(page, size);
    }

    public async Task<PersonDto> Find(int Id, CancellationToken cancellationToken)
    {
        if(await _uow.People.Exist(x=>x.Id == Id, cancellationToken))
        {
            var result = await _uow.People.Where(x=>x.Id == Id, cancellationToken);
            var entity = result.ToArray()[0];
            return _mapper.Map<Person, PersonDto>(entity);
        }
        throw new Exception("Este registro no existe.");
    }

    public async Task<PersonDto?> Update(PersonDto Dto, int Id, CancellationToken cancellationToken)
    {
        if(await _uow.People.Exist(x=>x.Id == Id, cancellationToken))
        {
            var entity = _mapper.Map<PersonDto, Person>(Dto);
            await _uow.People.Update(entity, cancellationToken);
            if (await _uow.Commit(cancellationToken))
            {
                return Dto;
            }
            else {
                await _uow.Rollback(cancellationToken);
                throw new Exception("No fue posible actualizar el registro.");
            }
        }
        throw new NullReferenceException("Esta Persona no existe.");
    }
}
