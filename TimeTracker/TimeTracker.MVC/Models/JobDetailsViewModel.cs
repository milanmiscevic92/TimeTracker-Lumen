using TimeTracker.Services.Types;

namespace TimeTracker.MVC.Models
{
    public class JobDetailsViewModel
    {
        public JobDto Job { get; set; }

        public Guid EmployeeToAddId { get; set; }

        public Guid EmployeeToRemoveId { get; set; }

        public List<EmployeeDto> EmployeesToAdd { get; set; } = new List<EmployeeDto>();
    }
}
