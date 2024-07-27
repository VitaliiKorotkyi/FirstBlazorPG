using EmployeeManagement.Model;

namespace BlazorServerAppLication.Services.Interface
{
    public interface IDepartmentService
    {
        public Task<IEnumerable<Department>> ShowAllDepartment();
        public Task<bool> RemoveDepartment(string name);
    }
}
