using TimeTracker.Services.Types;

namespace TimeTracker.MVC.Models
{
    public class JobListViewModel
    {
        public List<JobDto> Jobs { get; set; } = new List<JobDto>();
    }
}
