using AutoMapper;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Repositories.Interfaces;
using TimeTracker.Services.Interfaces;
using TimeTracker.Services.Types;

namespace TimeTracker.Services.Implementations
{
    public class JobService : IJobService
    {
        private readonly IMapper _mapper;
        private readonly IJobRepository _jobRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public JobService(IMapper mapper, IJobRepository jobRepository, IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _jobRepository = jobRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<List<JobDto>> GetAllAsync()
        {
            var jobsList = await _jobRepository.GetAllAsync();
            var result = new List<JobDto>();

            foreach (var job in jobsList)
            {
                result.Add(_mapper.Map<JobDto>(job));
            }

            return result;
        }

        public async Task<JobDto> GetByIdAsync(Guid id)
        {
            var job = await _jobRepository.GetByIdAsync(id);
            var result = _mapper.Map<JobDto>(job);

            return result;
        }

        public async Task<JobDto> CreateAsync(JobDto job)
        {
            var jobToCreate = _mapper.Map<Job>(job);
            var createdJob = await _jobRepository.CreateAsync(jobToCreate);
            var result = _mapper.Map<JobDto>(createdJob);

            return result;
        }

        public async Task<JobDto> UpdateAsync(JobDto job)
        {
            var jobToUpdate = _mapper.Map<Job>(job);
            var updatedJob = await _jobRepository.UpdateAsync(jobToUpdate);
            var result = _mapper.Map<JobDto>(updatedJob);

            return result;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _jobRepository.DeleteAsync(id);

            return result;
        }

        public async Task<Guid> AddEmployeeToJobAsync(Guid jobId, Guid employeeId)
        {
            var job = await _jobRepository.GetByIdAsync(jobId, false);
            var employee = await _employeeRepository.GetByIdAsync(employeeId, false);
            job.Employees.Add(employee);
            await _jobRepository.UpdateAsync(job);

            return jobId;
        }

        public async Task<Guid> RemoveEmployeeFromJobAsync(Guid jobId, Guid employeeId)
        {
            await _jobRepository.RemoveEmployeeFromJobAsync(jobId, employeeId);

            return jobId;
        }
    }
}
