using Domain.Entities;
using System.Linq.Expressions;
using Application.Utils;
using Application.Dtos;
using Application.Extensions;
using Domain.UnitsOfWork;
using Domain.Services;

namespace Application.UseCases;

public interface IPersonUseCase : IGenericUseCase<PersonDto>
{
    Task<IBasePagination<PersonDto>> Filter(string Search, int page, int size, CancellationToken cancellationToken);
}

public sealed class PersonUseCase : GenericUseCase<Person, PersonDto>, IPersonUseCase
{
    private readonly IGenericUnitOfWork _uow;
    private readonly IMapperService _mapper;

    public PersonUseCase(
            IGenericUnitOfWork UoW,
            IMapperService Mapper) : base(UoW, Mapper)
    {
        _uow = UoW;
        _mapper = Mapper;
    }

    public async Task<IBasePagination<PersonDto>> Filter(string Search, int page, int size, CancellationToken cancellationToken)
    {
        Expression<Func<Person, bool>> expression = x => x.Id > 0;

        if (!string.IsNullOrEmpty(Search) && !string.IsNullOrWhiteSpace(Search))
        {
            expression = expression.And(x => x.Name.Contains(Search));
        }

        var query = await _uow.GetRepository<Person>().Where(expression, cancellationToken);
        return query.Select(x => _mapper.Map<Person, PersonDto>(x)).Paginate(page, size);
    }
}
