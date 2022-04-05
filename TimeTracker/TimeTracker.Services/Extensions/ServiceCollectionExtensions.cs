using Microsoft.Extensions.DependencyInjection;
using TimeTracker.Services.Implementations;
using TimeTracker.Services.Interfaces;

namespace TimeTracker.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTimeTrackerCoreServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IJobService, JobService>()
                .AddScoped<IEmployeeService, EmployeeService>()
                .AddScoped<ITimeTrackingService, TimeTrackingService>()
                .AddScoped<IPrintingService, PrintingService>();
        }
    }
}
