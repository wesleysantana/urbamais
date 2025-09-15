using AutoMapper;
using Urbamais.Application.ViewModels.Request.v1.Unidade;
using Urbamais.Application.ViewModels.Response.v1.Unidade;
using Urbamais.Domain.Entities.Planejamento;

namespace Urbamais.CrossCutting.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Unidade
        CreateMap<UnidadeRequest, Unidade>();
        CreateMap<UnidadeUpdateRequest, Unidade>();
        CreateMap<Unidade, UnidadeResponse>();       
    }
}