using ApiServer.Mapper;
using ApiServer.ViewModels;
using Application.Common.Services;
using Domain.Entities;

namespace ApiServer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiServer(this IServiceCollection service)
        {
            service.AddTransient<IMapper<ProjectsViewModel, IEnumerable<Project>>, ProjectsViewModelMapper>();
            service.AddTransient<IMapper<RolesViewModel, IEnumerable<Role>>, RolesViewModelMapper>();
            service.AddTransient<IMapper<UserDetailsViewModel, User>, UserDetailsViewModelMapper>();

            return service;
        }
    }
}
