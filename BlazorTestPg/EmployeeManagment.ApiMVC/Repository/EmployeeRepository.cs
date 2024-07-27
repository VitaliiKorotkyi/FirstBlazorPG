
using EmployeeManagement.Model;
using EmployeeManagment.ApiMVC.Data;
using EmployeeManagment.ApiMVC.InterfaceRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace EmployeeManagment.ApiMVC.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(AppDbContext appDbContext, ILogger<EmployeeRepository> logger)
        {

            _appDbContext = appDbContext;
            _logger = logger;
        }
        public async Task<Employee> AddEmployee(Employee employee)
        {

            var newemployee = new Employee
            {
                EmployeeFirstName = employee.EmployeeFirstName,
                EmployeeLastName = employee.EmployeeLastName,
                EmployeeEmail = employee.EmployeeEmail,
                DateOfBirth = employee.DateOfBirth,
                Department = employee.Department,
                Gender = employee.Gender,
                PhotoPath = employee.PhotoPath
            };
            _appDbContext.Employees.Add(newemployee);
            await _appDbContext.SaveChangesAsync();
            //return CreatedAtAction(nameof(AddEmployee), new { id = newemployee.EmployeeId }, newemployee);
            return newemployee;
        }

        public async Task<Employee> DeleteEmployee(int employeeId)
        {
            var employee = await _appDbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            if (employee != null)
            {
                _appDbContext.Employees.Remove(employee);
                await _appDbContext.SaveChangesAsync();
                return employee;
            }
            return null;
        }

        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            var emplem = await _appDbContext.Employees.FirstOrDefaultAsync(n => n.EmployeeEmail == email);
            if (emplem != null)
            {
                return emplem;
            }
            return null;

        }

        public async Task<Employee> GetEmployeeById(int id)
        {

            return await _appDbContext.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async Task<Employee> GetEmployeeByName(string name)
        {
            var empl = _appDbContext.Employees.FirstOrDefault(n => n.EmployeeFirstName == name);
            if (empl.EmployeeFirstName == name || empl.EmployeeLastName == name)
            {
                return empl;
            }
            return null;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await _appDbContext.Employees.ToListAsync();
        }

        public async Task<IEnumerable<Employee>> SearchEmployee(string name, Gender? gender)
        {
            IQueryable<Employee> query = _appDbContext.Employees;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.EmployeeFirstName.Contains(name) || e.EmployeeLastName.Contains(name));
            }
            if (gender != null)
            {
                query = query.Where(e => e.Gender == gender);

            }
            return await query.ToListAsync();
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var existingEmployee = await _appDbContext.Employees.FindAsync(employee.EmployeeId);
            if (existingEmployee != null)
            {
                existingEmployee.EmployeeFirstName = employee.EmployeeFirstName;
                existingEmployee.EmployeeLastName = employee.EmployeeLastName;
                existingEmployee.EmployeeEmail = employee.EmployeeEmail;
                existingEmployee.DateOfBirth = employee.DateOfBirth;
                existingEmployee.Gender = employee.Gender;
                existingEmployee.PhotoPath = employee.PhotoPath;

                // Загрузите существующий Department из базы данных
                var existingDepartment = await _appDbContext.Departments.FindAsync(employee.Department.DepartmentId);
                if (existingDepartment != null)
                {
                    existingEmployee.Department = existingDepartment;
                }
                else
                {
                    existingEmployee.Department = employee.Department;
                }

                try
                {
                    await _appDbContext.SaveChangesAsync();
                    return existingEmployee;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error saving changes for employee ID {EmployeeId}", employee.EmployeeId);
                    throw; // Пробрасываем исключение дальше для обработки в контроллере
                }
            }
            else
            {
                _logger.LogWarning("Employee with ID {EmployeeId} not found", employee.EmployeeId);
                return null;
            }   
        }
    }
}
