using Domain.Entities;
using System.Linq.Expressions;
using Domain.Repositories;
using EasyMapper;
using Application.Utils;
using Application.Extensions;

namespace Application.UsesCases;

public interface IPersonUseCase : IGenericUseCase<PersonDto>
{
}

public sealed class PersonUseCase : IPersonUseCase
{
    private readonly IGenericRepository<Person> _repository;
    private readonly IMapper _mapper;

    public PersonUseCase(
            IGenericRepository<Person> Repository,
            IMapper Mapper)
    {
        _repository = Repository;
        _mapper = Mapper;
    }

    public async Task<PersonDto> Create(PersonDto Dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<PersonDto, Person>(Dto);
        await _repository.Add(entity, cancellationToken);
        if (await _repository.Commit(cancellationToken))
        {
            Dto.Id = entity.Id;
            return Dto;
        }
        {
            await _repository.Rollback(cancellationToken);
            throw new Exception("No fue posible crear este registro.");
        }
    }

    public async Task<int> Delete(int Id, CancellationToken cancellationToken)
    {
        if(await _repository.Exist(x=>x.Id == Id, cancellationToken))
        {
            await _repository.Delete(Id, cancellationToken);
            return await _repository.Commit(cancellationToken) ? Id : throw new Exception("No fue posible eliminar este registro.");
        }
        throw new NullReferenceException("Esta persona no existe.");
    }

    public async Task<IBasePagination<PersonDto>> Filter(Expression<Func<PersonDto, bool>> predicate, int page, int size, CancellationToken cancellationToken)
    {
        var query = await _repository.Select(x => new PersonDto
        {
            Id = x.Id,
            Name = x.Name
        }, predicate, cancellationToken);
        return query?.Paginate(page, size);
    }

    public async Task<PersonDto> Find(int Id, CancellationToken cancellationToken)
    {
        if(await _repository.Exist(x=>x.Id == Id, cancellationToken))
        {
            var result = await _repository.Select(x=>new PersonDto{
                Name = x.Name,
                Id=x.Id
            },x=>x.Id == Id, cancellationToken);
            return result.First();
        }
        throw new Exception("Este registro no existe.");
    }

    public async Task<PersonDto?> Update(PersonDto Dto, int Id, CancellationToken cancellationToken)
    {
        if(await _repository.Exist(x=>x.Id == Id, cancellationToken))
        {
            var entity = _mapper.Map<PersonDto, Person>(Dto);
            await _repository.Update(entity, cancellationToken);
            if (await _repository.Commit(cancellationToken))
            {
                return Dto;
            }
            else {
                await _repository.Rollback(cancellationToken);
                throw new Exception("No fue posible actualizar el registro.");
            }
        }
        throw new NullReferenceException("Esta Persona no existe.");
    }
}
