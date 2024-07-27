using BlazorServerAppLication.Services.Interface;
using BlazorServerAppLication.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Добавьте логирование
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient<IEmployeeService, EmployeeService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7220/");
});
builder.Services.AddHttpClient<IDepartmentService, DepartmentService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7220/");
});

//builder.Services.AddScoped<IEmployeeService, EmployeeService>();
//builder.Services.AddScoped<IDepartmentService, DepartmentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetService<ILogger<Program>>();
    try
    {
        await next.Invoke();
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An unexpected error occurred!");
        throw;
    }
});

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
