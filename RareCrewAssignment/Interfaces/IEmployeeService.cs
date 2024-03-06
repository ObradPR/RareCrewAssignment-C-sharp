using RareCrewAssignment.Models;

namespace RareCrewAssignment.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetEmployeesAsync();
    }
}
