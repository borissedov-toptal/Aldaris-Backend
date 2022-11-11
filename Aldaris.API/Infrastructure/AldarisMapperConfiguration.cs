using Aldaris.API.Domain;
using AutoMapper;

namespace Aldaris.API.Infrastructure;

public class AldarisMapperConfiguration : Profile
{
    public AldarisMapperConfiguration()
    {
        CreateMap<GameSession, GameSessionResponse>();
    }

    
    
}