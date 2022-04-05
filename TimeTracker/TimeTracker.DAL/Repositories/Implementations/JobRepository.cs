using Microsoft.EntityFrameworkCore;
using TimeTracker.DAL.Context;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Repositories.Interfaces;

namespace TimeTracker.DAL.Repositories.Implementations
{
    public class JobRepository : IJobRepository
    {
        private readonly TimeTrackerDbContext _context;

        public JobRepository(TimeTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Job>> GetAllAsync(bool includeEmployees = true)
        {
            try
            {
                var jobsQuery = _context.Set<Job>().AsQueryable();

                if (includeEmployees)
                {
                    jobsQuery = jobsQuery.Include(x => x.Employees);
                }

                var jobs = await jobsQuery.AsNoTracking().ToListAsync();

                return jobs;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to retrieve jobs: {ex.Message}");
            }
        }

        public async Task<Job> GetByIdAsync(Guid id, bool includeEmployees = true)
        {
            try
            {
                var jobQuery = _context.Set<Job>().AsQueryable();

                if (includeEmployees)
                {
                    jobQuery = jobQuery.Include(x => x.Employees);
                }

                var job = await jobQuery.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();

                if (job == null)
                {
                    throw new Exception($"Unable to find job with id={id}");
                }

                return job;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to retrieve job with id={id}: {ex.Message}");
            }
        }

        public async Task<Job> CreateAsync(Job job)
        {
            if (job == null)
            {
                throw new ArgumentNullException(nameof(job));
            }

            try
            {
                await _context.Set<Job>().AddAsync(job);
                await _context.SaveChangesAsync();

                return job;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to save a job: {ex.Message}");
            }
        }

        public async Task<Job> UpdateAsync(Job job)
        {
            if (job == null)
            {
                throw new ArgumentNullException(nameof(job));
            }

            try
            {
                _context.Set<Job>().Update(job);
                await _context.SaveChangesAsync();

                return job;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to update a job: {ex.Message}");
            }
        }

        public async Task<Job> RemoveEmployeeFromJobAsync(Guid jobId, Guid employeeId)
        {
            if (jobId == Guid.Empty || employeeId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(Guid));
            }

            var job = await _context.Set<Job>().Include(x => x.Employees).Where(x => x.Id == jobId).FirstOrDefaultAsync();

            if (job == null)
            {
                throw new Exception($"Unable to find a job to update.");
            }

            try
            {
                job.Employees.RemoveAll(x => x.Id == employeeId);
                _context.Set<Job>().Update(job);
                await _context.SaveChangesAsync();

                return job;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to update a job: {ex.Message}");
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var job = await _context.Set<Job>().FindAsync(id);

            if (job == null)
            {
                throw new Exception($"Unable to find a job.");
            }

            try
            {
                _context.Set<Job>().Remove(job);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to remove a job: {ex.Message}");
            }
        }
    }
}
