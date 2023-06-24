using AutoMapper;
using Urbamais.Application.ViewModels.Request.Unidade;
using Urbamais.Domain.Entities.Planejamento;

namespace Urbamais.CrossCutting.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Unidade, UnidadeRequest>();
    }
}