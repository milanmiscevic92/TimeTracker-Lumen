using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TimeTracker.DAL.Context;
using TimeTracker.DAL.Repositories.Implementations;
using TimeTracker.DAL.Repositories.Interfaces;

namespace TimeTracker.DAL.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTimeTrackerDataServices(this IServiceCollection services)
        {
            services.AddDbContext<TimeTrackerDbContext>(options => options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TimeTrackerDB;"));

            services
                .AddScoped<IJobRepository, JobRepository>()
                .AddScoped<IEmployeeRepository, EmployeeRepository>()
                .AddScoped<ITimeTrackingRepository, TimeTrackingRepository>();

            return services;
        }
    }
}
