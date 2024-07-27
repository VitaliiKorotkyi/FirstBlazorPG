using EmployeeManagement.Model;
using BlazorServerAppLication.Services.Interface;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BlazorServerAppLication.Pages
{
    public class AddNewEmployeeBase : ComponentBase
    {
        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        protected Employee NewEmployee { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            NewEmployee = new Employee
            {
                Department = new Department()  // Инициализация Department
            };
        }

        protected async Task HandleValidSubmit()
        {
            try
            {
                var addedEmployee = await EmployeeService.AddNewEmployee(NewEmployee);
                // Обработка добавленного сотрудника, например, навигация на другую страницу или отображение сообщения
                NavigationManager.NavigateTo("/employeelist");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Adding Employee: {ex.Message}");
            }
        }
    }
}
