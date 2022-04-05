using AutoMapper;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Repositories.Interfaces;
using TimeTracker.Services.Interfaces;
using TimeTracker.Services.Types;

namespace TimeTracker.Services.Implementations
{
    public class TimeTrackingService : ITimeTrackingService
    {
        private readonly IMapper _mapper;
        private readonly IJobService _jobService;
        private readonly IEmployeeService _employeeService;
        private readonly ITimeTrackingRepository _timeTrackingRepository;
        public TimeTrackingService(IMapper mapper, IJobService jobService, IEmployeeService employeeService, ITimeTrackingRepository timeTrackingRepository)
        {
            _mapper = mapper;
            _jobService = jobService;
            _employeeService = employeeService;
            _timeTrackingRepository = timeTrackingRepository;
        }

        public async Task<List<TimeTrackingDto>> GetTimeTrackingsByEmployeeId(Guid employeeId)
        {
            var timeTrackings = await _timeTrackingRepository.GetAllByEmployeeIdAsync(employeeId);
            var result = _mapper.Map<List<TimeTrackingDto>>(timeTrackings);

            return result;
        }

        public async Task<Guid> AddTrackedTimeAsync(Guid employeeId, Guid jobId, DateTime startTime, DateTime endTime)
        {
            var employee = await _employeeService.GetByIdAsync(employeeId);
            var job = await _jobService.GetByIdAsync(jobId);
            TimeSpan interval = endTime - startTime;

            var trackedTime = new TimeTrackingDto()
            {
                StartTime = startTime,
                EndTime = endTime,
                TotalHours = interval.TotalHours,
                TotalCost = interval.TotalHours * job.HourlyRate,
                EmployeeId = employee.Id,
                JobId = job.Id
            };

            var trackedTimeToCreate = _mapper.Map<TimeTracking>(trackedTime);
            await _timeTrackingRepository.CreateAsync(trackedTimeToCreate);

            return employeeId;
        }

        public async Task<double> CalculateEarningsForTimeInterval(Guid employeeId, Guid? jobId, DateTime startTime, DateTime endTime)
        {
            double totalEarnings = 0;
            IEnumerable<TimeTracking> trackedTimes;

            if (jobId == Guid.Empty)
            {
                trackedTimes = await _timeTrackingRepository.GetAllByEmployeeIdAsync(employeeId);
            }
            else
            {
                trackedTimes = await _timeTrackingRepository.GetAllByEmployeeIdAndJobIdAsync(employeeId, (Guid)jobId);
            }

            foreach (var trackedTime in trackedTimes)
            {
                if (trackedTime.StartTime >= startTime && trackedTime.EndTime <= endTime)
                {
                    totalEarnings = totalEarnings + trackedTime.TotalCost;
                }
            }

            return totalEarnings;
        }
    }
}
