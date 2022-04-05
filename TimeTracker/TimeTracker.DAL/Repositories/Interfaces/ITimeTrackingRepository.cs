using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.Repositories.Interfaces
{
    public interface ITimeTrackingRepository
    {
        public Task<IEnumerable<TimeTracking>> GetAllAsync(bool includeEmployee = true, bool includeJob = true);

        public Task<TimeTracking> GetByIdAsync(Guid id, bool includeEmployee = true, bool includeJob = true);

        public Task<IEnumerable<TimeTracking>> GetAllByEmployeeIdAsync(Guid employeeId);

        public Task<IEnumerable<TimeTracking>> GetAllByEmployeeIdAndJobIdAsync(Guid employeeId, Guid jobId);

        public Task<TimeTracking> CreateAsync(TimeTracking timeTracking);

        public Task<TimeTracking> UpdateAsync(TimeTracking timeTracking);

        public Task<bool> DeleteAsync(Guid id);
    }
}
