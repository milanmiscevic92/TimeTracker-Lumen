using Microsoft.EntityFrameworkCore;
using TimeTracker.DAL.Context;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Repositories.Interfaces;

namespace TimeTracker.DAL.Repositories.Implementations
{
    public class TimeTrackingRepository : ITimeTrackingRepository
    {
        private readonly TimeTrackerDbContext _context;

        public TimeTrackingRepository(TimeTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TimeTracking>> GetAllAsync(bool includeEmployee = true, bool includeJob = true)
        {
            try
            {
                var trackedTimesQuery = _context.Set<TimeTracking>().AsQueryable();

                if (includeEmployee)
                {
                    trackedTimesQuery = trackedTimesQuery.Include(x => x.Employee);
                }

                if (includeJob)
                {
                    trackedTimesQuery = trackedTimesQuery.Include(x => x.Job);
                }

                var trackedTimes = await trackedTimesQuery.AsNoTracking().ToListAsync();

                return trackedTimes;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to retrieve tracked times: {ex.Message}");
            }
        }

        public async Task<TimeTracking> GetByIdAsync(Guid id, bool includeEmployee = true, bool includeJob = true)
        {
            try
            {
                var trackedTimeQuery = _context.Set<TimeTracking>().AsQueryable();

                if (includeEmployee)
                {
                    trackedTimeQuery = trackedTimeQuery.Include(x => x.Employee);
                }

                if (includeJob)
                {
                    trackedTimeQuery = trackedTimeQuery.Include(x => x.Job);
                }

                var trackedTime = await trackedTimeQuery.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();

                if (trackedTime == null)
                {
                    throw new Exception($"Unable to find tracked time with id={id}");
                }

                return trackedTime;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to retrieve tracked time with id={id}: {ex.Message}");
            }
        }

        public async Task<IEnumerable<TimeTracking>> GetAllByEmployeeIdAsync(Guid employeeId)
        {
            try
            {
                var trackedTimes = await _context
                    .Set<TimeTracking>()
                    .Include(x => x.Employee)
                    .Include(x => x.Job)
                    .AsNoTracking()
                    .Where(x => x.EmployeeId == employeeId)
                    .ToListAsync();

                if (trackedTimes == null)
                {
                    throw new Exception($"Unable to find tracked times for employee {employeeId}");
                }

                return trackedTimes;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to retrieve tracked times for employee {employeeId}: {ex.Message}");
            }
        }

        public async Task<IEnumerable<TimeTracking>> GetAllByEmployeeIdAndJobIdAsync(Guid employeeId, Guid jobId)
        {
            try
            {
                var trackedTimes = await _context
                    .Set<TimeTracking>()
                    .AsNoTracking()
                    .Where(x => x.EmployeeId == employeeId && x.JobId == jobId)
                    .ToListAsync();

                if (trackedTimes == null)
                {
                    throw new Exception($"Unable to find tracked times for employee {employeeId} and job {jobId}");
                }

                return trackedTimes;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to retrieve tracked times for employee {employeeId} and job {jobId}: {ex.Message}");
            }
        }

        public async Task<TimeTracking> CreateAsync(TimeTracking trackedTime)
        {
            if (trackedTime == null)
            {
                throw new ArgumentNullException(nameof(trackedTime));
            }

            try
            {
                await _context.Set<TimeTracking>().AddAsync(trackedTime);
                await _context.SaveChangesAsync();

                return trackedTime;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to save tracked time: {ex.Message}");
            }
        }

        public async Task<TimeTracking> UpdateAsync(TimeTracking trackedTime)
        {
            if (trackedTime == null)
            {
                throw new ArgumentNullException(nameof(trackedTime));
            }

            try
            {
                _context.Set<TimeTracking>().Update(trackedTime);
                await _context.SaveChangesAsync();

                return trackedTime;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to update tracked time: {ex.Message}");
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var trackedTime = await _context.Set<TimeTracking>().FindAsync(id);

            if (trackedTime == null)
            {
                throw new Exception($"Unable to find tracked time.");
            }

            try
            {
                _context.Set<TimeTracking>().Remove(trackedTime);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to remove tracked time: {ex.Message}");
            }
        }
    }
}
