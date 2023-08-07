using AutoMapper;
using Urbamais.Application.ViewModels.Request.V1.City;
using Urbamais.Application.ViewModels.Request.V1.Companie;
using Urbamais.Application.ViewModels.Request.V1.Input;
using Urbamais.Application.ViewModels.Request.V1.Unit;
using Urbamais.Application.ViewModels.Response.V1.City;
using Urbamais.Application.ViewModels.Response.V1.Companie;
using Urbamais.Application.ViewModels.Response.V1.Input;
using Urbamais.Application.ViewModels.Response.V1.Unit;
using Urbamais.Domain.Entities.Obras;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.Entities.Planejamentos;

namespace Urbamais.CrossCutting.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // City
        CreateMap<CityRequest, Cidade>();
        CreateMap<CityUpdateRequest, Empresa>();
        CreateMap<Cidade, CityResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Nome.Value));

        // Companie
        CreateMap<CompanieRequest, Empresa>();
        CreateMap<CompanieUpdateRequest, Empresa>();
        CreateMap<Empresa, CompanieResponse>()
            .ForMember(dest => dest.CorporateName, opt => opt.MapFrom(src => src.RazaoSocial.Value))
            .ForMember(dest => dest.TradeName, opt => opt.MapFrom(src => src.NomeFantasia.Value))
            .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(src => src.Cnpj.Value));
                
        // Input
        CreateMap<InputRequest, Insumo>();
        CreateMap<InputUpdateRequest, Insumo>();
        CreateMap<Insumo, InputResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Nome.Value))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Descricao.Value));

        // Unity
        CreateMap<UnitRequest, Unidade>();
        CreateMap<UnitUpdateRequest, Unidade>();
        CreateMap<Unidade, UnitResponse>();
    }
}