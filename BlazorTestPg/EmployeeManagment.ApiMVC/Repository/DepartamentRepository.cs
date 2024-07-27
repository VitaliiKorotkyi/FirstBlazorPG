using EmployeeManagement.Model;
using EmployeeManagment.ApiMVC.Data;
using EmployeeManagment.ApiMVC.InterfaceRepository;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagment.ApiMVC.Repository
{
    public class DepartamentRepository : IDepartamentRepository
    {
        private readonly AppDbContext _appDbContext;
        public DepartamentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Department> DeleteDepartament(string name)
        {
            var deldep = await GetByDepartamentName(name);
            if (deldep is null)
            {
                return null;
            }
            _appDbContext.Departments.Remove(deldep);
            _appDbContext.SaveChangesAsync();
            return deldep;

        }

        public async Task<Department> GetByDepartamentId(int departamentId)
         => await _appDbContext.Departments.FirstOrDefaultAsync(e => e.DepartmentId == departamentId);


        public async Task<Department> GetByDepartamentName(string name)
      => await _appDbContext.Departments.FirstOrDefaultAsync(n => n.DepartmentName == name);

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await _appDbContext.Departments.ToListAsync();
        }
    }
}
