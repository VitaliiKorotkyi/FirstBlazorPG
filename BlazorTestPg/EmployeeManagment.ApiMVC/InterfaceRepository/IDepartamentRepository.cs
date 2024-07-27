using EmployeeManagement.Model;

namespace EmployeeManagment.ApiMVC.InterfaceRepository
{
    public interface IDepartamentRepository
    {
        Task<IEnumerable<Department>> GetDepartments();
        Task<Department> GetByDepartamentId(int departamentId);
        Task<Department> GetByDepartamentName(string name);
        Task<Department> DeleteDepartament(string name);

    }
}
