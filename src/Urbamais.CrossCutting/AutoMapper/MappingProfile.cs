using AutoMapper;
using Urbamais.Application.ViewModels.Request.V1.Cidade;
using Urbamais.Application.ViewModels.Request.V1.Empresa;
using Urbamais.Application.ViewModels.Request.V1.Insumo;
using Urbamais.Application.ViewModels.Request.V1.Unidade;
using Urbamais.Application.ViewModels.Response.V1.Cidade;
using Urbamais.Application.ViewModels.Response.V1.Empresa;
using Urbamais.Application.ViewModels.Response.V1.Insumo;
using Urbamais.Application.ViewModels.Response.V1.Unidade;
using Urbamais.Domain.Entities.Obras;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.Entities.Planejamentos;

namespace Urbamais.CrossCutting.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // City
        CreateMap<CidadeRequest, Cidade>();
        CreateMap<CidadeUpdateRequest, Empresa>();
        CreateMap<Cidade, CidadeResponse>()
            .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome.Value));

        // Companie
        CreateMap<EmpresaRequest, Empresa>();
        CreateMap<EmpresaUpdateRequest, Empresa>();
        CreateMap<Empresa, EmpresaResponse>()
            .ForMember(dest => dest.RazaoSocial, opt => opt.MapFrom(src => src.RazaoSocial.Value))
            .ForMember(dest => dest.NomeFantasia, opt => opt.MapFrom(src => src.NomeFantasia.Value))
            .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(src => src.Cnpj.Value));
                
        // Input
        CreateMap<InsumoRequest, Insumo>();
        CreateMap<InsumoUpdateRequest, Insumo>();
        CreateMap<Insumo, InsumoResponse>()
            .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome.Value))
            .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao.Value));

        // Unity
        CreateMap<UnidadeRequest, Unidade>();
        CreateMap<UnidadeUpdateRequest, Unidade>();
        CreateMap<Unidade, UnidadeResponse>();
    }
}