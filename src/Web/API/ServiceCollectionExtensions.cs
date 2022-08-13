using ApiServer.Mapper;
using ApiServer.ViewModels;
using Domain.Entities;

namespace ApiServer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiServer(this IServiceCollection service)
        {
            return service.AddTransient<IMapper<ProjectsViewModel, IEnumerable<Project>>, ProjectsViewModelMapper>();
        }
    }
}
