using BlazorServerAppLication.Services.Interface;
using EmployeeManagement.Model;

namespace BlazorServerAppLication.Services
{
    public class DepartmentService:IDepartmentService
    {
        private readonly HttpClient _httpClient;

        public DepartmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Department>> ShowAllDepartment()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Department>>("api/departments");
        }
        public async Task<bool> RemoveDepartment(string name)
        {
            var response = await _httpClient.DeleteAsync($"api/department/{name}");
            return response.IsSuccessStatusCode;
        }
    }
}
