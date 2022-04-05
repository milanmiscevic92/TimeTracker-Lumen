namespace TimeTracker.DAL.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Job> Jobs { get; set; } = new List<Job>();

        public List<TimeTracking> TrackedTimes { get; set; } = new List<TimeTracking>();
    }
}
