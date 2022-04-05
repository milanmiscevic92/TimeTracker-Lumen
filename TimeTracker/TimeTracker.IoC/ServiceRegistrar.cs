using Microsoft.Extensions.DependencyInjection;
using TimeTracker.DAL.Extensions;
using TimeTracker.Services.Extensions;

namespace TimeTracker.IoC
{
    public class ServiceRegistrar : IServiceRegistrar
    {
        public IServiceCollection AddTimeTrackerServices(IServiceCollection services)
        {
            return services
                .AddTimeTrackerDataServices()
                .AddTimeTrackerCoreServices();
        }
    }
}
