using API.Mapper;
using API.ViewModels;
using Domain.Entities;

namespace API
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiServer(this IServiceCollection service)
        {
            return service.AddTransient<IMapper<ProjectsViewModel,IEnumerable<Project>>,ProjectsViewModelMapper>();
        }
    }
}
