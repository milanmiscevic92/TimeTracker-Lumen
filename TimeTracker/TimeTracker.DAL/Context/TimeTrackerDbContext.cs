using Microsoft.EntityFrameworkCore;
using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.Context
{
    public class TimeTrackerDbContext : DbContext
    {
        public TimeTrackerDbContext(DbContextOptions<TimeTrackerDbContext> options) : base(options) { }

        public DbSet<Job> Jobs { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<TimeTracking> TrackedTimes { get; set; }
    }
}
