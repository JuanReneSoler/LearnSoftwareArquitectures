using Domain.Services;
using EasyMapper;

namespace Infrastructure.Services;

public class MapperService : IMapperService
{
    private readonly IMapper _mapper;
    public MapperService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public TOutput Map<TInput, TOutput>(TInput Entity)
    {
        return _mapper.Map<TInput, TOutput>(Entity);
    }
}
