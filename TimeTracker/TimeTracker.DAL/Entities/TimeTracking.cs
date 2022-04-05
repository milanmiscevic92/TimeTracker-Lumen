namespace TimeTracker.DAL.Entities
{
    public class TimeTracking
    {
        public Guid Id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public double TotalHours { get; set; }

        public double TotalCost { get; set; }

        public Guid EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public Guid JobId { get; set; }

        public Job Job { get; set; }
    }
}
