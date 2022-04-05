using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimeTracker.MVC.Models;
using TimeTracker.Services.Interfaces;

namespace TimeTracker.MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IPrintingService _printingService;
        private readonly ITimeTrackingService _timeTrackingService;

        public EmployeeController(IEmployeeService employeeService, IPrintingService printingService, ITimeTrackingService timeTrackingService)
        {
            _employeeService = employeeService;
            _printingService = printingService;
            _timeTrackingService = timeTrackingService;
        }

        [HttpGet]
        public async Task<IActionResult> ManageEmployees()
        {
            EmployeeListViewModel vm = new EmployeeListViewModel()
            {
                Employees = await _employeeService.GetAllAsync()
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            EmployeeCreateViewModel vm = new EmployeeCreateViewModel();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateViewModel vm)
        {
            await _employeeService.CreateAsync(vm.Employee);

            return RedirectToAction("ManageEmployees");
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id, double? totalEarnings = null)
        {
            var trackedTimes = await _timeTrackingService.GetTimeTrackingsByEmployeeId(id);
            var employee = await _employeeService.GetByIdAsync(id);
            List<SelectListItem> jobs = new List<SelectListItem>();

            foreach (var job in employee.Jobs)
            {
                jobs.Add(new SelectListItem() { Value = job.Id.ToString(), Text = job.Title });
            }

            EmployeeDetailsViewModel vm = new EmployeeDetailsViewModel()
            {
                Employee = await _employeeService.GetByIdAsync(id),
                Jobs = jobs,
                TotalEarnings = totalEarnings,
                TrackedTimes = trackedTimes
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            EmployeeUpdateViewModel vm = new EmployeeUpdateViewModel()
            {
                Employee = await _employeeService.GetByIdAsync(id)
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeUpdateViewModel vm)
        {
            var result = await _employeeService.UpdateAsync(vm.Employee);

            return RedirectToAction("Edit", new { id = result.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _employeeService.DeleteAsync(id);

            return RedirectToAction("ManageEmployees");
        }

        [HttpPost]
        public async Task<IActionResult> AddTrackedTime(EmployeeDetailsViewModel vm)
        {
            var result = await _timeTrackingService.AddTrackedTimeAsync(vm.Employee.Id, Guid.Parse(vm.SelectedJobId), vm.SelectedStartTime, vm.SelectedEndTime);

            return RedirectToAction("Details", new { id = result });
        }

        [HttpPost]
        public async Task<IActionResult> CalculateEariningsForTimeInterval(EmployeeDetailsViewModel vm)
        {
            var selectedJobId = vm.SelectedJobId == "All jobs" ? Guid.Empty : Guid.Parse(vm.SelectedJobId);
            var result = await _timeTrackingService.CalculateEarningsForTimeInterval(vm.Employee.Id, selectedJobId, vm.SelectedStartTime, vm.SelectedEndTime);

            return RedirectToAction("Details", new { id = vm.Employee.Id, totalEarnings = result });
        }

        [HttpPost]
        public async Task<IActionResult> PrintEmployeeDetails(Guid employeeId)
        {
            var document = await _printingService.PrintAsPdf(employeeId);

            return File(document.Data, System.Net.Mime.MediaTypeNames.Application.Octet, document.DocumentName);
        }
    }
}
