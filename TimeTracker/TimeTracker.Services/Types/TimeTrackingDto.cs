namespace TimeTracker.Services.Types
{
    public class TimeTrackingDto
    {
        public Guid Id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public double TotalHours { get; set; }

        public double TotalCost { get; set; }

        public Guid EmployeeId { get; set; }

        public EmployeeDto Employee { get; set; }

        public Guid JobId { get; set; }

        public JobDto Job { get; set; }
    }
}
