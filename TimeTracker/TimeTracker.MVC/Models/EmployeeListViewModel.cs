using TimeTracker.Services.Types;

namespace TimeTracker.MVC.Models
{
    public class EmployeeListViewModel
    {
        public List<EmployeeDto> Employees { get; set; } = new List<EmployeeDto>();
    }
}
