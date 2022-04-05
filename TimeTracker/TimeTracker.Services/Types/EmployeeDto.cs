using TimeTracker.DAL.Entities;

namespace TimeTracker.Services.Types
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<JobDto> Jobs { get; set; } = new List<JobDto>();

        public List<TimeTrackingDto> TrackedTimes { get; set; } = new List<TimeTrackingDto>();
    }
}
