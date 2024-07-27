using EmployeeManagement.Model;
using EmployeeManagment.ApiMVC.InterfaceRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

namespace EmployeeManagment.ApiMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeesController> _logger;
        public EmployeesController(IEmployeeRepository employeeRepository, ILogger<EmployeesController> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult> ShowAllEmployees()
        {
            try
            {
                var employees = await _employeeRepository.GetEmployeesAsync();
                Console.WriteLine($"Employees: {JsonConvert.SerializeObject(employees)}"); // Логирование для отладки
                return Ok(employees);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }

        }
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateNewEmployee(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest("Employee data is invalid.");
                }

                var existingEmployee = await _employeeRepository.GetEmployeeByEmail(employee.EmployeeEmail);
                if (existingEmployee != null)
                {
                    ModelState.AddModelError("email", "Employee email already in use");
                    return BadRequest(ModelState);
                }

                var createdEmployee = await _employeeRepository.AddEmployee(employee);
                return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.EmployeeId }, createdEmployee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        {
            try
            {
                if (id != employee.EmployeeId)
                {
                    _logger.LogWarning("Employee ID mismatch: {Id} != {EmployeeId}", id, employee.EmployeeId);
                    return BadRequest("Employee ID mismatch");
                }

                var employeeToUpdate = await _employeeRepository.GetEmployeeById(id);
                if (employeeToUpdate == null)
                {
                    _logger.LogWarning("Employee with ID {Id} not found", id);
                    return NotFound($"Employee with ID {id} not found");
                }

                var updatedEmployee = await _employeeRepository.UpdateEmployee(employee);
                if (updatedEmployee == null)
                {
                    _logger.LogError("Error updating employee data for ID {Id}", id);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error updating employee data");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating employee data for ID {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }

        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {
                var empltoDelet = await _employeeRepository.GetEmployeeById(id);
                if (empltoDelet is null)
                {
                    return NotFound($"Employee with this id = {id} is exist");
                }
                var deletedEmployee = await _employeeRepository.DeleteEmployee(id);
                return Ok(deletedEmployee);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }

        }
        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Employee>>> Search(string name,Gender? gender)
        {
            try
            {
           var result=   await  _employeeRepository.SearchEmployee(name,gender);
                if (result.Any())
                { 
                return Ok(result);
                }
                return NotFound();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeById(id);
                if (employee == null)
                {
                    return NotFound();
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

    }
}
