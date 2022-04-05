using Microsoft.Extensions.DependencyInjection;

namespace TimeTracker.IoC
{
    public interface IServiceRegistrar
    {
        IServiceCollection AddTimeTrackerServices(IServiceCollection services);
    }
}
