using LocaFilme.App.Extensions;
using LocaFilme.Business.Interfaces;
using LocaFilme.Business.Notificacoes;
using LocaFilme.Business.Services;
using LocaFilme.Data.Context;
using LocaFilme.Data.Repository;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace LocaFilme.App.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MeuDbContext>();
            services.AddScoped<IFilmeRepository, FilmeRepository>();
            services.AddScoped<ILocacaoRepository, LocacaoRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();

            services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<ILocacaoService, LocacaoService>();
            services.AddScoped<IFilmeService, FilmeService>();

            return services;
        }
    }
}