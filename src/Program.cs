




// Swagger + Services

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using src.DeviceEmployeeAuthManager.DAL;
using src.DeviceEmployeeAuthManager.Helpers.Options;
using src.DeviceEmployeeAuthManager.Middlewares;
// using src.DeviceEmployeeAuthManager.Repositories;
using src.DeviceEmployeeAuthManager.Services;
using src.DeviceEmployeeAuthManager.Services.Tokens;

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
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
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
        ValidateIssuer = true,   //by who
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
            {
                context.Response.Headers.Add("Token-expired", "true");
            }
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