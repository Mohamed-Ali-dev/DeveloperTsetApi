using DeveloperTestApi.Data;
using DeveloperTestApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
    throw new InvalidOperationException("No Connection String was found");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    ControllerActionEndpointConventionBuilder controllerActionEndpointConventionBuilder = endpoints.MapControllers(); // API endpoints
    PageActionEndpointConventionBuilder pageActionEndpointConventionBuilder = endpoints.MapRazorPages();   // Add this if using Razor Pages
});
app.UseHttpsRedirection();


app.MapControllers();
app.UseStaticFiles();

app.Run();
