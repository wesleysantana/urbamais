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
using Urbamais.Domain.Entities.Financeiro;
using Urbamais.Application.ViewModels.Request.V1.RegistroFinanceiro;
using Urbamais.Application.ViewModels.Response.V1.RegistroFinanceiro;
using Urbamais.Application.ViewModels.Request.V1.CentroCusto;
using Urbamais.Application.ViewModels.Response.V1.CentroCusto;

namespace Urbamais.CrossCutting.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Centro de Custo
        CreateMap<CentroCustoRequest, CentroCusto>();
        CreateMap<CentroCustoUpdateRequest, CentroCusto>();
        CreateMap<CentroCusto, CentroCustoResponse>()
            .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao.Value));

        // Cidade
        CreateMap<CidadeRequest, Cidade>();
        CreateMap<CidadeUpdateRequest, Empresa>();
        CreateMap<Cidade, CidadeResponse>()
            .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome.Value));

        // Empresa
        CreateMap<EmpresaRequest, Empresa>();
        CreateMap<EmpresaUpdateRequest, Empresa>();
        CreateMap<Empresa, EmpresaResponse>()
            .ForMember(dest => dest.RazaoSocial, opt => opt.MapFrom(src => src.RazaoSocial.Value))
            .ForMember(dest => dest.NomeFantasia, opt => opt.MapFrom(src => src.NomeFantasia.Value))
            .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(src => src.Cnpj.Value));
                
        // Insumo
        CreateMap<InsumoRequest, Insumo>();
        CreateMap<InsumoUpdateRequest, Insumo>();
        CreateMap<Insumo, InsumoResponse>()
            .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome.Value))
            .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao.Value));

        // Registro Financeiro
        CreateMap<RegistroFinanceiroRequest, RegistroFinanceiro>();
        CreateMap<RegistroFinanceiroUpdateRequest, RegistroFinanceiro>();
        CreateMap<RegistroFinanceiro, RegistroFinanceiroResponse>()
            .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Valor.Value))
            .ForMember(dest => dest.Caucao, opt => opt.MapFrom(src => src.Valor.Value))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Valor.Value))
            .ForMember(dest => dest.Desconto, opt => opt.MapFrom(src => src.Valor.Value))
            .ForMember(dest => dest.Acrescimo, opt => opt.MapFrom(src => src.Valor.Value))
            .ForMember(dest => dest.ValorLiquido, opt => opt.MapFrom(src => src.Valor.Value))
            .ForMember(dest => dest.ValorBaixa, opt => opt.MapFrom(src => src.Valor.Value));

        // Unidade
        CreateMap<UnidadeRequest, Unidade>();
        CreateMap<UnidadeUpdateRequest, Unidade>();
        CreateMap<Unidade, UnidadeResponse>();
    }
}