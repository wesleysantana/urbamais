using AutoMapper;
using Urbamais.Application.ViewModels.Request.V1.City;
using Urbamais.Application.ViewModels.Request.V1.Companie;
using Urbamais.Application.ViewModels.Request.V1.Input;
using Urbamais.Application.ViewModels.Request.V1.Unit;
using Urbamais.Application.ViewModels.Response.V1.City;
using Urbamais.Application.ViewModels.Response.V1.Companie;
using Urbamais.Application.ViewModels.Response.V1.Input;
using Urbamais.Application.ViewModels.Response.V1.Unit;
using Urbamais.Domain.Entities.Construction;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.Entities.Planning;

namespace Urbamais.CrossCutting.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // City
        CreateMap<CityRequest, City>();
        CreateMap<CityUpdateRequest, Companie>();
        CreateMap<City, CityResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Nome.Value));

        // Companie
        CreateMap<CompanieRequest, Companie>();
        CreateMap<CompanieUpdateRequest, Companie>();
        CreateMap<Companie, CompanieResponse>()
            .ForMember(dest => dest.CorporateName, opt => opt.MapFrom(src => src.CorporateName.Value))
            .ForMember(dest => dest.TradeName, opt => opt.MapFrom(src => src.TradeName.Value))
            .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(src => src.Cnpj.Value));
                
        // Input
        CreateMap<InputRequest, Input>();
        CreateMap<InputUpdateRequest, Input>();
        CreateMap<Input, InputResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Value))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Value));

        // Unity
        CreateMap<UnitRequest, Unit>();
        CreateMap<UnitUpdateRequest, Unit>();
        CreateMap<Unit, UnitResponse>();
    }
}