using AutoMapper;
using LocaFilme.App.ViewModels;
using LocaFilme.Business.Models;

namespace LocaFilme.App.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Locacao, LocacaoViewModel>().ReverseMap();
            CreateMap<Cliente, ClienteViewModel>().ReverseMap();
            CreateMap<Filme, FilmeViewModel>().ReverseMap();
        }
    }
}