using EmployeeManagement.Model;
using BlazorServerAppLication.Services.Interface;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorServerAppLication.Pages
{
    public class EditEmployeeBase : ComponentBase
    {
        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Inject]
        public IDepartmentService DepartmentService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public Employee Employee { get; set; } = new Employee();
        public List<Department> Departments { get; set; } = new List<Department>();

        [Parameter]
        public int Id { get; set; } // Изменено на int
        public bool IsLoading { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (Id != 0)
                {
                    Employee = await EmployeeService.GetEmployeeById(Id);
                    if (Employee == null)
                    {
                        Console.WriteLine($"Employee with ID {Id} not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid or missing employee ID.");
                }

                // Добавляем отладочные сообщения и обработку исключений
                try
                {
                    Console.WriteLine("Before calling ShowAllDepartment");
                    var departments = await DepartmentService.ShowAllDepartment();
                    Departments = departments.ToList();
                    Console.WriteLine("After calling ShowAllDepartment");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error calling ShowAllDepartment: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving employee data: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        protected async Task HandleValidSubmit()
        {
            try
            {
                if (Employee != null)
                {
                    var success = await EmployeeService.UpdateEmployee(Employee);
                    if (success)
                    {
                        // Перенаправление на страницу со списком всех сотрудников после сохранения
                        NavigationManager.NavigateTo("/employeelist");
                    }
                    else
                    {
                        Console.WriteLine("Employee update failed.");
                    }
                }
                else
                {
                    Console.WriteLine("Employee data is null.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating employee data: {ex.Message}");
            }
        }
    }
}
