using AutoMapper;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Repositories.Interfaces;
using TimeTracker.Services.Interfaces;
using TimeTracker.Services.Types;

namespace TimeTracker.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IMapper mapper, IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }

        public async Task<List<EmployeeDto>> GetAllAsync()
        {
            var employeesList = await _employeeRepository.GetAllAsync();
            var result = new List<EmployeeDto>();

            foreach (var employee in employeesList)
            {
                result.Add(_mapper.Map<EmployeeDto>(employee));
            }

            return result;
        }

        public async Task<EmployeeDto> GetByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            var result = _mapper.Map<EmployeeDto>(employee);

            return result;
        }

        public async Task<EmployeeDto> CreateAsync(EmployeeDto employee)
        {
            var employeeToCreate = _mapper.Map<Employee>(employee);
            var createdEmployee = await _employeeRepository.CreateAsync(employeeToCreate);
            var result = _mapper.Map<EmployeeDto>(createdEmployee);

            return result;
        }

        public async Task<EmployeeDto> UpdateAsync(EmployeeDto job)
        {
            var employeeToUpdate = _mapper.Map<Employee>(job);
            var updatedEmployee = await _employeeRepository.UpdateAsync(employeeToUpdate);
            var result = _mapper.Map<EmployeeDto>(updatedEmployee);

            return result;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _employeeRepository.DeleteAsync(id);

            return result;
        }
    }
}
