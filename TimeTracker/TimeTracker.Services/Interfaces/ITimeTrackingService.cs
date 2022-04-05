using TimeTracker.Services.Types;

namespace TimeTracker.Services.Interfaces
{
    public interface ITimeTrackingService
    {
        public Task<List<TimeTrackingDto>> GetTimeTrackingsByEmployeeId(Guid employeeId);

        public Task<Guid> AddTrackedTimeAsync(Guid employeeId, Guid jobId, DateTime startTime, DateTime endTime);

        public Task<double> CalculateEarningsForTimeInterval(Guid employeeId, Guid? jobId, DateTime startTime, DateTime endTime);
    }
}
