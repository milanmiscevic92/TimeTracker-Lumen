using Microsoft.AspNetCore.Mvc.Rendering;
using TimeTracker.Services.Types;

namespace TimeTracker.MVC.Models
{
    public class EmployeeDetailsViewModel
    {
        public EmployeeDto Employee { get; set; }

        public string SelectedJobId { get; set; }

        public DateTime SelectedStartTime { get; set; }

        public DateTime SelectedEndTime { get; set; }

        public double? TotalEarnings { get; set; }

        public List<SelectListItem> Jobs { get; set; } = new List<SelectListItem>();

        public List<TimeTrackingDto> TrackedTimes { get; set; } = new List<TimeTrackingDto>();
    }
}
