using EmployeeManagement.Model;
using Microsoft.AspNetCore.Components;

namespace BlazorServerAppLication.Pages
{
    public class DisplayEmployeeBase:ComponentBase
    {
        [Parameter]
        public Employee Employee { get; set; }
        [Parameter]
        public bool ShowFooter { get; set; }
        [Parameter]
        public EventCallback<bool> OnEmployeeSalection { get; set; }
        protected async Task CheckBoxChenged(ChangeEventArgs e)
        {
          await  OnEmployeeSalection.InvokeAsync((bool)e.Value);
        }
    }
}
