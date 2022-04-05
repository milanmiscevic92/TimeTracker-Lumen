using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetAllAsync(bool includeJobs = true);

        public Task<Employee> GetByIdAsync(Guid id, bool includeJobs = true, bool includeTrackedTimes = true);

        public Task<Employee> CreateAsync(Employee employee);

        public Task<Employee> UpdateAsync(Employee employee);

        public Task<bool> DeleteAsync(Guid id);
    }
}
