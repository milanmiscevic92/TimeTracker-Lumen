using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.Repositories.Interfaces
{
    public interface IJobRepository
    {
        public Task<IEnumerable<Job>> GetAllAsync(bool includeEmployees = true);

        public Task<Job> GetByIdAsync(Guid id, bool includeEmployees = true);

        public Task<Job> CreateAsync(Job job);

        public Task<Job> UpdateAsync(Job job);

        public Task<bool> DeleteAsync(Guid id);

        public Task<Job> RemoveEmployeeFromJobAsync(Guid jobId, Guid employeeId);
    }
}
