using TimeTracker.Services.Types;

namespace TimeTracker.Services.Interfaces
{
    public interface IEmployeeService
    {
        public Task<List<EmployeeDto>> GetAllAsync();

        public Task<EmployeeDto> GetByIdAsync(Guid id);

        public Task<EmployeeDto> CreateAsync(EmployeeDto employee);

        public Task<EmployeeDto> UpdateAsync(EmployeeDto employee);

        public Task<bool> DeleteAsync(Guid id);
    }
}
