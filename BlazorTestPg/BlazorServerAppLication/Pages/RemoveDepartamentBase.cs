using BlazorServerAppLication.Services.Interface;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BlazorServerAppLication.Pages
{
    public class RemoveDepartamentBase : ComponentBase
    {
        [Inject]
        public IDepartmentService DepartmentService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string Name { get; set; }

        public bool IsSuccessful { get; set; }
        public bool IsLoading { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    IsSuccessful = await DepartmentService.RemoveDepartment(Name);
                }
                else
                {
                    IsSuccessful = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing department: {ex.Message}");
                IsSuccessful = false;
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
