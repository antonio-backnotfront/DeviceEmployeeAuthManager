// Swagger + Services

using System;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DeviceEmployeeAuthManager.API.DAL;
using DeviceEmployeeAuthManager.API.Helpers.Config;
using DeviceEmployeeAuthManager.API.Helpers.Options;
using DeviceEmployeeAuthManager.API.Middlewares;
using DeviceEmployeeAuthManager.API.Services;
using DeviceEmployeeAuthManager.API.Services.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

// using DeviceEmployeeAuthManager.DeviceEmployeeAuthManager.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var jwtConfigData = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<JwtOptions>(jwtConfigData);


var connectionString = builder.Configuration.GetConnectionString("DefaultDatabase");
builder.Services.AddDbContext<DeviceEmployeeDbContext>(opt => opt.UseSqlServer(connectionString));

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<IDeviceValidator, DeviceValidator>();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// !======== basic authentication (login + password) ========!
// app.UseMiddleware<BasicAuthMiddleware>();

// !======== AspNetCore.Identity ========!
// builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
// {
//     options.Password.RequiredLength = 12;
//     options.Password.RequireUppercase = true;
//     options.Password.RequireLowercase = true;
//     options.Password.RequireNonAlphanumeric = true;
//     options.Password.RequireDigit = true;
// })
//     .AddEntityFrameworkStores<DeviceEmployeeDbContext>()
//     .AddDefaultTokenProviders();

// !======== jwt(json web token) bearer (access token) ========!
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, //by who
        ValidateAudience = true, //for whom
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(10),
        ValidIssuer = jwtConfigData["Issuer"], //should come from configuration
        ValidAudience = jwtConfigData["Audience"], //should come from configuration
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfigData["Key"]))
    };

    opt.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                context.Response.Headers.Add("Token-expired", "true");
            return Task.CompletedTask;
        }
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<DeviceMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();