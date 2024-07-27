using BlazorServerAppLication.Services.Interface;
using EmployeeManagement.Model;

namespace BlazorServerAppLication.Services
{
    public class EmployeeService : IEmployeeService
    {
        public readonly HttpClient _httpClient;
        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Employee> AddNewEmployee(Employee employee)
        {
            var response = await _httpClient.PostAsJsonAsync("api/employees", employee);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Employee>();


        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Employee>>("api/employees");
        }

       

        public async Task<Employee> GetEmployeeById(int id)
        {
            var response = await _httpClient.GetAsync($"api/employees/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Employee>();
            }
            else
            {
                throw new Exception($"Error retrieving employee data: {response.ReasonPhrase}");
            }
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/employees/{employee.EmployeeId}", employee);
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Response status code does not indicate success: {response.StatusCode} ({response.ReasonPhrase}). {errorMessage}");
            }
            return true;
        }

    }
}
