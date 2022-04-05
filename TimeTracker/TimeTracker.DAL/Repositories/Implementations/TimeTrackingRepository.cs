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
                var timeTrackingsQuery = _context.Set<TimeTracking>().AsQueryable();

                if (includeEmployee)
                {
                    timeTrackingsQuery = timeTrackingsQuery.Include(x => x.Employee);
                }

                if (includeJob)
                {
                    timeTrackingsQuery = timeTrackingsQuery.Include(x => x.Job);
                }

                var timeTrackings = await timeTrackingsQuery.AsNoTracking().ToListAsync();

                return timeTrackings;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to retrieve time trackings: {ex.Message}");
            }
        }

        public async Task<TimeTracking> GetByIdAsync(Guid id, bool includeEmployee = true, bool includeJob = true)
        {
            try
            {
                var timeTrackingQuery = _context.Set<TimeTracking>().AsQueryable();

                if (includeEmployee)
                {
                    timeTrackingQuery = timeTrackingQuery.Include(x => x.Employee);
                }

                if (includeJob)
                {
                    timeTrackingQuery = timeTrackingQuery.Include(x => x.Job);
                }

                var timeTracking = await timeTrackingQuery.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();

                if (timeTracking == null)
                {
                    throw new Exception($"Unable to find time tracking with id={id}");
                }

                return timeTracking;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to retrieve time tracking with id={id}: {ex.Message}");
            }
        }

        public async Task<IEnumerable<TimeTracking>> GetAllByEmployeeIdAsync(Guid employeeId)
        {
            try
            {
                var timeTrackings = await _context
                    .Set<TimeTracking>()
                    .Include(x => x.Employee)
                    .Include(x => x.Job)
                    .AsNoTracking()
                    .Where(x => x.EmployeeId == employeeId)
                    .ToListAsync();

                if (timeTrackings == null)
                {
                    throw new Exception($"Unable to find time trackings for employee {employeeId}");
                }

                return timeTrackings;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to retrieve time trackings for employee {employeeId}: {ex.Message}");
            }
        }

        public async Task<IEnumerable<TimeTracking>> GetAllByEmployeeIdAndJobIdAsync(Guid employeeId, Guid jobId)
        {
            try
            {
                var timeTrackings = await _context
                    .Set<TimeTracking>()
                    .AsNoTracking()
                    .Where(x => x.EmployeeId == employeeId && x.JobId == jobId)
                    .ToListAsync();

                if (timeTrackings == null)
                {
                    throw new Exception($"Unable to find time trackings for employee {employeeId} and job {jobId}");
                }

                return timeTrackings;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to retrieve time trackings for employee {employeeId} and job {jobId}: {ex.Message}");
            }
        }

        public async Task<TimeTracking> CreateAsync(TimeTracking timeTracking)
        {
            if (timeTracking == null)
            {
                throw new ArgumentNullException(nameof(timeTracking));
            }

            try
            {
                await _context.Set<TimeTracking>().AddAsync(timeTracking);
                await _context.SaveChangesAsync();

                return timeTracking;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to save a time tracking: {ex.Message}");
            }
        }

        public async Task<TimeTracking> UpdateAsync(TimeTracking timeTracking)
        {
            if (timeTracking == null)
            {
                throw new ArgumentNullException(nameof(timeTracking));
            }

            try
            {
                _context.Set<TimeTracking>().Update(timeTracking);
                await _context.SaveChangesAsync();

                return timeTracking;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to update a time tracking: {ex.Message}");
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var timeTracking = await _context.Set<TimeTracking>().FindAsync(id);

            if (timeTracking == null)
            {
                throw new Exception($"Unable to find a time tracking.");
            }

            try
            {
                _context.Set<TimeTracking>().Remove(timeTracking);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to remove a time tracking: {ex.Message}");
            }
        }
    }
}
