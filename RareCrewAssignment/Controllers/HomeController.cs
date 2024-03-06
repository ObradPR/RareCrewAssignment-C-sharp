using Microsoft.AspNetCore.Mvc;
using RareCrewAssignment.Interfaces;
using RareCrewAssignment.Models;
using RareCrewAssignment.Services;
using System.Diagnostics;

namespace RareCrewAssignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public HomeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetEmployeesAsync();

            foreach (var employee in employees)
            {
                if (employee.EmployeeName == null) continue;

                var timestamp = employee.EndTimeUtc - employee.StarTimeUtc;

                var timestampTicks = timestamp.Ticks;

                if (timestampTicks <= 0) continue;

                var minutes = (int)timestamp.TotalMinutes;

                employee.TotalMinutes = minutes;
            }

            var uniqueEmployees = employees
                .GroupBy(x => x.EmployeeName)
                .Select(s => new Employee
                {
                    EmployeeName = s.Key,
                    TotalHours = s.Sum(m => m.TotalMinutes) / 60,
                    TotalMinutes = s.Sum(m => m.TotalMinutes)
                })
                .Where(w => w.EmployeeName != null)
                .OrderByDescending(o => o.TotalHours)
                .ToList();

            return View(uniqueEmployees);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
