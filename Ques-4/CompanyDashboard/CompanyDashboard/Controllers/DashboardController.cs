using CompanyDashboard.Hubs;
using CompanyDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CompanyDashboard.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<UpdateHub> _hubContext;

        public DashboardController(ApplicationDbContext context, IHubContext<UpdateHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var result = _context.Departments
                                .Select(dept => new
                                {
                                    DepartmentName = dept.DepartmentName,
                                    EmployeeCount = dept.Employees.Count()
                                })
                                .ToList();
            return View(result);
        }



        [HttpPost]
        public async Task<IActionResult> CreateDepartment(DepartmentModel departmentData)
        {
            await _context.Departments.AddAsync(departmentData);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveUpdate");
            return Ok(new { msg = "New department added" });

        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeModel empData)
        {
            await _context.Employees.AddAsync(empData);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveUpdate");
            return Ok(new { msg = "New employee added" });

        }


        [HttpGet]
        public IActionResult GetEmployeeCountByDepartmentApi()
        {
            var result = _context.Departments
                                .Select(dept => new
                                {
                                    DepartmentName = dept.DepartmentName,
                                    EmployeeCount = dept.Employees.Count()
                                })
                                .ToList();
            return Ok(result);
        }


    }
}
