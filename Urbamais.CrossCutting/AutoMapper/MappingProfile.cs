using AutoMapper;
using Urbamais.Application.ViewModels.Request.v1.Input;
using Urbamais.Application.ViewModels.Request.v1.Unit;
using Urbamais.Application.ViewModels.Response.v1.Input;
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
        CreateMap<Input, InputResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Description));

        // Unity
        CreateMap<UnitRequest, Unit>();
        CreateMap<UnitUpdateRequest, Unit>();
        CreateMap<Unit, UnitResponse>();
    }
}