using Microsoft.Extensions.DependencyInjection;

namespace TimeTracker.IoC
{
    public static class ServiceRegistrations
    {
        public static IServiceRegistrar Implementation { private get; set; } = new ServiceRegistrar();

        public static IServiceCollection AddTimeTrackerServices(this IServiceCollection services)
        {
            return Implementation.AddTimeTrackerServices(services);
        }
    }
}
