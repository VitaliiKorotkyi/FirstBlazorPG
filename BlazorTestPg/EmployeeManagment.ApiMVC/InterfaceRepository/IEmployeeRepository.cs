using EmployeeManagement.Model;

namespace EmployeeManagment.ApiMVC.InterfaceRepository
{
    public interface IEmployeeRepository
    {

        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<IEnumerable<Employee>> SearchEmployee(string name,Gender? gender);
        Task<Employee> GetEmployeeById(int id);
        Task<Employee> GetEmployeeByName(string name);
        Task<Employee> GetEmployeeByEmail(string name);
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);
        Task<Employee> DeleteEmployee(int employeeid);



    }
}
