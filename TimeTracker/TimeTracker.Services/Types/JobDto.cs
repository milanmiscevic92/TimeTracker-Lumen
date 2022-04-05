namespace TimeTracker.Services.Types
{
    public class JobDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public double HourlyRate { get; set; }

        public List<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();

        public List<TimeTrackingDto> TrackedTimes { get; set; } = new List<TimeTrackingDto>();
    }
}
