using RareCrewAssignment.Interfaces;
using RareCrewAssignment.Models;
using System.Text.Json;

namespace RareCrewAssignment.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public EmployeeService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            var apiKey = _configuration["ApiKey"];

            var res = await _httpClient.GetAsync($"https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code={apiKey}");

            res.EnsureSuccessStatusCode();

            var data = await res.Content.ReadAsStringAsync();

            var opt = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var employees = JsonSerializer.Deserialize<List<Employee>>(data, opt);

            if (employees == null)
                throw new NullReferenceException("Data can't be null");

            return employees;
        }
    }
}
