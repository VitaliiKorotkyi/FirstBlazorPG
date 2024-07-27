using BlazorServerAppLication.Services.Interface;
using EmployeeManagement.Model;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerAppLication.Pages
{
    public class ShowAllDepartamentBase : ComponentBase
    {
        [Inject]
        public IDepartmentService DepartmentService { get; set; }

        public List<Department> Departments { get; set; } = new List<Department>();
        public bool IsLoading { get; set; } = true;
        public bool HasError { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {

            Departments=(await DepartmentService.ShowAllDepartment()).ToList();
            //Console.WriteLine("OnInitializedAsync started.");
            //try
            //{
            //    IsLoading = true;
            //    var departments = await DepartmentService.ShowAllDepartment();
            //    Departments = departments.ToList();
            //    Console.WriteLine($"Departments loaded successfully. Count: {Departments.Count}");
            //    foreach (var department in Departments)
            //    {
            //        Console.WriteLine($"Department: {department.DepartmentName}");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    HasError = true;
            //    ErrorMessage = ex.Message;
            //    Console.WriteLine($"Error retrieving departments: {ex.Message}");
            //}
            //finally
            //{
            //    IsLoading = false;
            //    Console.WriteLine("OnInitializedAsync finished.");
            //}
        }
    }
}
