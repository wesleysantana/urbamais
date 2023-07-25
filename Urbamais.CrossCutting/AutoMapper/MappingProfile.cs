using AutoMapper;
using Urbamais.Application.ViewModels.Request.v1.Input;
using Urbamais.Application.ViewModels.Request.v1.Unit;
using Urbamais.Application.ViewModels.Response.v1.Unit;
using Urbamais.Domain.Entities.Planning;

namespace Urbamais.CrossCutting.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Input
        CreateMap<InputRequest, Input>();
        CreateMap<InputUpdateRequest, Input>();

        // Unity
        CreateMap<UnitRequest, Unit>();
        CreateMap<UnitUpdateRequest, Unit>();
        CreateMap<Unit, UnitResponse>();
    }
}