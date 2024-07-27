using BlazorServerAppLication.Services.Interface;
using EmployeeManagement.Model;
using Microsoft.AspNetCore.Components;

namespace BlazorServerAppLication.Pages
{
    public class EmployeeDetailsBase : ComponentBase
    {
        public Employee Employee { get; set; } = new Employee();
        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        [Parameter]
        public string Id { get; set; }

        protected string ButtonText { get; set; } = "Hide Footer";
        protected string CssClass { get; set; } = null;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Id = Id ?? "1";

                if (int.TryParse(Id, out int employeeId))
                {
                    Employee = await EmployeeService.GetEmployeeById(employeeId);
                }
                else
                {
                    Console.WriteLine("Invalid employee ID format.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving employee data: {ex.Message}");
            }
        }

        protected void Button_Click()
        {
            if (ButtonText == "Hide Footer")
            {
                ButtonText = "Show Footer";
                CssClass = "hide-footer";
            }
            else
            {
                CssClass = null;
                ButtonText = "Hide Footer";
            }
        }
    }
}
