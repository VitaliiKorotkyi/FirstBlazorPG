using EmployeeManagement.Model;

namespace BlazorServerAppLication.Services.Interface
{
    public interface IEmployeeService
    {
        public Task<IEnumerable<Employee>> GetAllEmployees();
        public Task<Employee> GetEmployeeById(int id);
        public Task<Employee> AddNewEmployee(Employee employee);
        public Task<bool> UpdateEmployee(Employee employee);
      
    }
}
