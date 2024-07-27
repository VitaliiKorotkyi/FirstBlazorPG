using EmployeeManagement.Model;
using EmployeeManagment.ApiMVC.InterfaceRepository;
using EmployeeManagment.ApiMVC.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagment.ApiMVC.Controllers
{ 
    [Route("api/[controller]")]
        [ApiController]
    public class DepartmentController : ControllerBase
    {
      private readonly IDepartamentRepository _departamentRepository;
        public DepartmentController(IDepartamentRepository departamentRepository)
        { 
        _departamentRepository = departamentRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> ShowAllDepartment()
        {
            var departments = await _departamentRepository.GetDepartments();
            if (departments == null)
            {
                return NotFound();
            }
            return Ok(departments);
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult> DeleteDepartment(string name)
        {
            var success = await _departamentRepository.DeleteDepartament(name);
            if (success is null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
