using TimeTracker.Services.Types;

namespace TimeTracker.Services.Interfaces
{
    public interface IJobService
    {
        public Task<List<JobDto>> GetAllAsync();

        public Task<JobDto> GetByIdAsync(Guid id);

        public Task<JobDto> CreateAsync(JobDto job);

        public Task<JobDto> UpdateAsync(JobDto job);

        public Task<bool> DeleteAsync(Guid id);

        public Task<Guid> AddEmployeeToJobAsync(Guid jobId, Guid employeeId);

        public Task<Guid> RemoveEmployeeFromJobAsync(Guid jobId, Guid employeeId);
    }
}
