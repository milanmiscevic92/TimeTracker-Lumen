using Microsoft.EntityFrameworkCore;
using TimeTracker.DAL.Context;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Repositories.Interfaces;

namespace TimeTracker.DAL.Repositories.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly TimeTrackerDbContext _context;

        public EmployeeRepository(TimeTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(bool includeJobs = true)
        {
            try
            {
                var employeesQuery = _context.Set<Employee>().AsQueryable();

                if (includeJobs)
                {
                    employeesQuery = employeesQuery.Include(x => x.Jobs);
                }

                var employees = await employeesQuery.AsNoTracking().ToListAsync();

                return employees;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to retrieve employees: {ex.Message}");
            }
        }

        public async Task<Employee> GetByIdAsync(Guid id, bool includeJobs = true, bool includeTrackedTimes = true)
        {
            try
            {
                var employeeQuery = _context.Set<Employee>().AsQueryable();

                if (includeJobs)
                {
                    employeeQuery = employeeQuery.Include(x => x.Jobs);
                }

                if (includeTrackedTimes)
                {
                    employeeQuery = employeeQuery.Include(x => x.TrackedTimes);
                }

                var employee = await employeeQuery.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();

                if (employee == null)
                {
                    throw new Exception($"Unable to find employee with id={id}");
                }

                return employee;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to retrieve employee with id={id}: {ex.Message}");
            }
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            try
            {
                await _context.Set<Employee>().AddAsync(employee);
                await _context.SaveChangesAsync();

                return employee;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to save an employee: {ex.Message}");
            }
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            try
            {
                _context.Set<Employee>().Update(employee);
                await _context.SaveChangesAsync();

                return employee;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to update an employee: {ex.Message}");
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var employee = await _context.Set<Employee>().FindAsync(id);

            if (employee == null)
            {
                throw new Exception($"Unable to find an employee.");
            }

            try
            {
                _context.Set<Employee>().Remove(employee);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to remove an employee: {ex.Message}");
            }
        }
    }
}
