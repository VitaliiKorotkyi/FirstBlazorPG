using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace EmployeeManagement.Model
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        [MinLength(2)]
        public string EmployeeFirstName { get; set; }

        [Required]
        [MinLength(2)]
        public string EmployeeLastName { get; set; }

        [Required]
        [EmailAddress]
        public string EmployeeEmail { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public int DepartmentId { get; set; }

        public string PhotoPath { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public Department Department { get; set; } = new Department();
    }
}
