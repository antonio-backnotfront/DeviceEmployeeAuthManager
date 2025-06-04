




// Swagger + Services

using Microsoft.EntityFrameworkCore;
using src.DeviceEmployeeAuthManager.DAL;
using src.DeviceEmployeeAuthManager.Repositories;
using src.DeviceEmployeeAuthManager.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultDatabase");
builder.Services.AddDbContext<DeviceEmployeeDbContext>(opt => opt.UseSqlServer(connectionString));

builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<IDeviceService, DeviceService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();