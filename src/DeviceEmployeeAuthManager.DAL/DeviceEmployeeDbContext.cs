using Microsoft.EntityFrameworkCore;
using src.DeviceEmployeeAuthManager.Models;

namespace src.DeviceEmployeeAuthManager.DAL;

public class DeviceEmployeeDbContext : DbContext
{
    public DbSet<Role> Roles { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<DeviceType> DeviceTypes { get; set; }
    public DbSet<DeviceEmployee> DeviceEmployees { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<Account> Accounts { get; set; }

    protected DeviceEmployeeDbContext() { }
    public DeviceEmployeeDbContext(DbContextOptions options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<DeviceType>().HasData(
        new DeviceType { Id = 1, Name = "PC" },
        new DeviceType { Id = 2, Name = "Smartwatch" },
        new DeviceType { Id = 3, Name = "Embedded" },
        new DeviceType { Id = 4, Name = "Monitor" },
        new DeviceType { Id = 5, Name = "Printer" }
    );

    modelBuilder.Entity<Position>().HasData(
        new Position { Id = 1, Name = "Software Engineer", MinExpYears = 2 },
        new Position { Id = 2, Name = "System Administrator", MinExpYears = 3 },
        new Position { Id = 3, Name = "Project Manager", MinExpYears = 5 },
        new Position { Id = 4, Name = "IT Support", MinExpYears = 1 },
        new Position { Id = 5, Name = "HR Manager", MinExpYears = 4 }
    );

    modelBuilder.Entity<Person>().HasData(
        new Person { Id = 1, PassportNumber = "AA123456", FirstName = "John", MiddleName = "Michael", LastName = "Doe", PhoneNumber = "+1234567890", Email = "john.doe@example.com" },
        new Person { Id = 2, PassportNumber = "BB654321", FirstName = "Jane", MiddleName = null, LastName = "Smith", PhoneNumber = "+1987654321", Email = "jane.smith@example.com" },
        new Person { Id = 3, PassportNumber = "CC987654", FirstName = "Alice", MiddleName = "Marie", LastName = "Johnson", PhoneNumber = "+1123456789", Email = "alice.johnson@example.com" },
        new Person { Id = 4, PassportNumber = "DD246810", FirstName = "Bob", MiddleName = null, LastName = "Brown", PhoneNumber = "+1222333444", Email = "bob.brown@example.com" },
        new Person { Id = 5, PassportNumber = "EE135791", FirstName = "Eve", MiddleName = "Grace", LastName = "Davis", PhoneNumber = "+1333555777", Email = "eve.davis@example.com" }
    );

    modelBuilder.Entity<Device>().HasData(
        new Device { Id = 1, Name = "HP ProDesk 600 G6", IsEnabled = false, AdditionalProperties = "{\"operationSystem\": null}", DeviceTypeId = 1 },
        new Device { Id = 2, Name = "ThinkCentre M75q Gen 5 Tiny", IsEnabled = true, AdditionalProperties = "{\"operationSystem\": \"Windows 11 Pro 23H2\"}", DeviceTypeId = 1 },
        new Device { Id = 3, Name = "Apple Watch Ultra 2", IsEnabled = true, AdditionalProperties = "{\"battery\": \"24%\"}", DeviceTypeId = 2 },
        new Device { Id = 4, Name = "Raspberry Pi 4", IsEnabled = false, AdditionalProperties = "{\"ipAddress\": \"192.168.0.1\", \"network\": \"example\"}", DeviceTypeId = 3 },
        new Device { Id = 5, Name = "Dell UltraSharp", IsEnabled = true, AdditionalProperties = "{\"ports\": [{\"type\": \"HDMI\", \"version\": \"2.0\"}]}", DeviceTypeId = 4 },
        new Device { Id = 6, Name = "HP LaserJet", IsEnabled = false, AdditionalProperties = "{\"colors\": \"black and white\"}", DeviceTypeId = 5 }
    );

    modelBuilder.Entity<Employee>().HasData(
        new Employee { Id = 1, Salary = 75000.00m, PositionId = 1, PersonId = 1, HireDate = new DateTime(2021, 3, 1) },
        new Employee { Id = 2, Salary = 65000.00m, PositionId = 2, PersonId = 2, HireDate = new DateTime(2022, 6, 15) },
        new Employee { Id = 3, Salary = 90000.00m, PositionId = 3, PersonId = 3, HireDate = new DateTime(2020, 1, 20) },
        new Employee { Id = 4, Salary = 55000.00m, PositionId = 4, PersonId = 4, HireDate = new DateTime(2023, 9, 10) },
        new Employee { Id = 5, Salary = 80000.00m, PositionId = 5, PersonId = 5, HireDate = new DateTime(2019, 12, 1) }
    );

    modelBuilder.Entity<DeviceEmployee>().HasData(
        new DeviceEmployee { Id = 1, DeviceId = 1, EmployeeId = 1, IssueDate = new DateTime(2023, 1, 10), ReturnDate = null },
        new DeviceEmployee { Id = 2, DeviceId = 2, EmployeeId = 2, IssueDate = new DateTime(2023, 2, 20), ReturnDate = new DateTime(2024, 1, 15) },
        new DeviceEmployee { Id = 3, DeviceId = 3, EmployeeId = 3, IssueDate = new DateTime(2022, 8, 5), ReturnDate = null },
        new DeviceEmployee { Id = 4, DeviceId = 4, EmployeeId = 4, IssueDate = new DateTime(2023, 11, 1), ReturnDate = null },
        new DeviceEmployee { Id = 5, DeviceId = 5, EmployeeId = 5, IssueDate = new DateTime(2021, 5, 25), ReturnDate = new DateTime(2023, 5, 25) }
    );
    
    modelBuilder.Entity<Role>().HasData(
        new Role { Id = 1, Name = "Admin" },
        new Role { Id = 2, Name = "User" }
    );

    modelBuilder.Entity<Account>().HasData(
        new Account
        {
            Id = 1,
            Username = "admin.john",
            Password = "hashedpassword1", // Replace with hashed value if applicable
            EmployeeId = 1,
            RoleId = 1
        },
        new Account
        {
            Id = 2,
            Username = "jane.sys",
            Password = "hashedpassword2",
            EmployeeId = 2,
            RoleId = 2
        },
        new Account
        {
            Id = 3,
            Username = "alice.pm",
            Password = "hashedpassword3",
            EmployeeId = 3,
            RoleId = 2
        },
        new Account
        {
            Id = 4,
            Username = "bob.support",
            Password = "hashedpassword4",
            EmployeeId = 4,
            RoleId = 2
        },
        new Account
        {
            Id = 5,
            Username = "eve.hr",
            Password = "hashedpassword5",
            EmployeeId = 5,
            RoleId = 1
        }
    );

}

}