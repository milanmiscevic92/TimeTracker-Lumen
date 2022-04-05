namespace TimeTracker.DAL.Entities
{
    public class Job
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public double HourlyRate { get; set; }

        public List<Employee> Employees { get; set; } = new List<Employee>();

        public List<TimeTracking> TrackedTimes { get; set; } = new List<TimeTracking>();
    }
}
