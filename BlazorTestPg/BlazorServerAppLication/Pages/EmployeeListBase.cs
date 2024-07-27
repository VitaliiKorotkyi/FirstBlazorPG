using BlazorServerAppLication.Services.Interface;
using EmployeeManagement.Model;
using Microsoft.AspNetCore.Components;

namespace BlazorServerAppLication.Pages
{
    public class EmployeeListBase : ComponentBase
    {
        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public bool ShowFooter { get; set; } = true;
        protected override async Task OnInitializedAsync()
        {
          Employees =  (await EmployeeService.GetAllEmployees()).ToList();
       
        }
        protected int SelectedEmploeesCount { get; set; } = 0;
        protected void EmployeeSelectionChanged(bool isSelected)
        {
            if (isSelected)
            {

                SelectedEmploeesCount++;
            
            }
            else
            {
                SelectedEmploeesCount--;
            }
        }

        //private void LoadEmployees()
        //{
        //    Thread.Sleep(3000); 
        //    var employees = new List<Employee>
        //    {
        //        new Employee
        //        {
        //            EmployeeId = 1,
        //            EmployeeFirstName = "Bob",
        //            EmployeeLastName = "Drob",
        //            EmployeeEmail = "BobDrob@mail.cz",
        //            DateOfBirth = new DateTime(1983, 4, 23),
        //            Department = new Department { DepartmentId = 3, DepartmentName = "IT" },
        //            Gender = Gender.Male,
        //            PhotoPath = "image/bob.jpg"
        //        },
        //        new Employee
        //        {
        //            EmployeeId = 2,
        //            EmployeeFirstName = "Alice",
        //            EmployeeLastName = "Smith",
        //            EmployeeEmail = "AliceSmith@mail.cz",
        //            DateOfBirth = new DateTime(1990, 5, 12),
        //            Department = new Department { DepartmentId = 1, DepartmentName = "HR" },
        //            Gender = Gender.Female,
        //            PhotoPath = "image/alice.jpg"
        //        }
        //    };

        //    Employees = employees;
        //}

    }
}