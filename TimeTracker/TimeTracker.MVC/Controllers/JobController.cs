using Microsoft.AspNetCore.Mvc;
using TimeTracker.MVC.Models;
using TimeTracker.Services.Interfaces;

namespace TimeTracker.MVC.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobService _jobService;
        private readonly IEmployeeService _employeeService;

        public JobController(IJobService jobService, IEmployeeService employeeService)
        {
            _jobService = jobService;
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> ManageJobs()
        {
            JobListViewModel vm = new JobListViewModel()
            {
                Jobs = await _jobService.GetAllAsync()
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            JobCreateViewModel vm = new JobCreateViewModel();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(JobCreateViewModel vm)
        {
            await _jobService.CreateAsync(vm.Job);

            return RedirectToAction("ManageJobs");
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var job = await _jobService.GetByIdAsync(id);
            var allEmployees = await _employeeService.GetAllAsync();
            var employeesToAdd = allEmployees.Where(x => !x.Jobs.Any(y => y.Id == job.Id)).ToList();

            JobDetailsViewModel vm = new JobDetailsViewModel()
            {
                Job = job,
                EmployeesToAdd = employeesToAdd
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            JobUpdateViewModel vm = new JobUpdateViewModel()
            {
                Job = await _jobService.GetByIdAsync(id)
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(JobUpdateViewModel vm)
        {
            var result = await _jobService.UpdateAsync(vm.Job);

            return RedirectToAction("Edit", new { id = result.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _jobService.DeleteAsync(id);

            return RedirectToAction("ManageJobs");
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeToJob(JobDetailsViewModel vm)
        {
            var result = await _jobService.AddEmployeeToJobAsync(vm.Job.Id, vm.EmployeeToAddId);

            return RedirectToAction("Details", new { id = result });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveEmployeeFromJob(JobDetailsViewModel vm)
        {
            var result = await _jobService.RemoveEmployeeFromJobAsync(vm.Job.Id, vm.EmployeeToRemoveId);

            return RedirectToAction("Details", new { id = result });
        }
    }
}
