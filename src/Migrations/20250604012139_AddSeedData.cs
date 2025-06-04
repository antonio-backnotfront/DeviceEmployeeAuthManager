using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace src.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Device_DeviceType_DeviceTypeId",
                table: "Device");

            migrationBuilder.DropForeignKey(
                name: "FK_DeviceEmployee_Device_DeviceId",
                table: "DeviceEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_DeviceEmployee_Employee_EmployeeId",
                table: "DeviceEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Person_PersonId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Position_PositionId",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Position",
                table: "Position");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Person",
                table: "Person");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeviceType",
                table: "DeviceType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeviceEmployee",
                table: "DeviceEmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Device",
                table: "Device");

            migrationBuilder.RenameTable(
                name: "Position",
                newName: "Positions");

            migrationBuilder.RenameTable(
                name: "Person",
                newName: "Persons");

            migrationBuilder.RenameTable(
                name: "DeviceType",
                newName: "DeviceTypes");

            migrationBuilder.RenameTable(
                name: "DeviceEmployee",
                newName: "DeviceEmployees");

            migrationBuilder.RenameTable(
                name: "Device",
                newName: "Devices");

            migrationBuilder.RenameIndex(
                name: "IX_DeviceEmployee_EmployeeId",
                table: "DeviceEmployees",
                newName: "IX_DeviceEmployees_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_DeviceEmployee_DeviceId",
                table: "DeviceEmployees",
                newName: "IX_DeviceEmployees_DeviceId");

            migrationBuilder.RenameIndex(
                name: "IX_Device_DeviceTypeId",
                table: "Devices",
                newName: "IX_Devices_DeviceTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Positions",
                table: "Positions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                table: "Persons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeviceTypes",
                table: "DeviceTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeviceEmployees",
                table: "DeviceEmployees",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Devices",
                table: "Devices",
                column: "Id");

            migrationBuilder.InsertData(
                table: "DeviceTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "PC" },
                    { 2, "Smartwatch" },
                    { 3, "Embedded" },
                    { 4, "Monitor" },
                    { 5, "Printer" }
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "MiddleName", "PassportNumber", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", "John", "Doe", "Michael", "AA123456", "+1234567890" },
                    { 2, "jane.smith@example.com", "Jane", "Smith", null, "BB654321", "+1987654321" },
                    { 3, "alice.johnson@example.com", "Alice", "Johnson", "Marie", "CC987654", "+1123456789" },
                    { 4, "bob.brown@example.com", "Bob", "Brown", null, "DD246810", "+1222333444" },
                    { 5, "eve.davis@example.com", "Eve", "Davis", "Grace", "EE135791", "+1333555777" }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "MinExpYears", "Name" },
                values: new object[,]
                {
                    { 1, 2, "Software Engineer" },
                    { 2, 3, "System Administrator" },
                    { 3, 5, "Project Manager" },
                    { 4, 1, "IT Support" },
                    { 5, 4, "HR Manager" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" }
                });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "AdditionalProperties", "DeviceTypeId", "IsEnabled", "Name" },
                values: new object[,]
                {
                    { 1, "{\"operationSystem\": null}", 1, false, "HP ProDesk 600 G6" },
                    { 2, "{\"operationSystem\": \"Windows 11 Pro 23H2\"}", 1, true, "ThinkCentre M75q Gen 5 Tiny" },
                    { 3, "{\"battery\": \"24%\"}", 2, true, "Apple Watch Ultra 2" },
                    { 4, "{\"ipAddress\": \"192.168.0.1\", \"network\": \"example\"}", 3, false, "Raspberry Pi 4" },
                    { 5, "{\"ports\": [{\"type\": \"HDMI\", \"version\": \"2.0\"}]}", 4, true, "Dell UltraSharp" },
                    { 6, "{\"colors\": \"black and white\"}", 5, false, "HP LaserJet" }
                });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "HireDate", "PersonId", "PositionId", "Salary" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 75000.00m },
                    { 2, new DateTime(2022, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 65000.00m },
                    { 3, new DateTime(2020, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, 90000.00m },
                    { 4, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4, 55000.00m },
                    { 5, new DateTime(2019, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 5, 80000.00m }
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "EmployeeId", "Password", "RoleId", "Username" },
                values: new object[,]
                {
                    { 1, 1, "hashedpassword1", 1, "admin.john" },
                    { 2, 2, "hashedpassword2", 2, "jane.sys" },
                    { 3, 3, "hashedpassword3", 2, "alice.pm" },
                    { 4, 4, "hashedpassword4", 2, "bob.support" },
                    { 5, 5, "hashedpassword5", 1, "eve.hr" }
                });

            migrationBuilder.InsertData(
                table: "DeviceEmployees",
                columns: new[] { "Id", "DeviceId", "EmployeeId", "IssueDate", "ReturnDate" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, 2, 2, new DateTime(2023, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, 3, new DateTime(2022, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 4, 4, 4, new DateTime(2023, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 5, 5, 5, new DateTime(2021, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceEmployees_Devices_DeviceId",
                table: "DeviceEmployees",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceEmployees_Employee_EmployeeId",
                table: "DeviceEmployees",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_DeviceTypes_DeviceTypeId",
                table: "Devices",
                column: "DeviceTypeId",
                principalTable: "DeviceTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Persons_PersonId",
                table: "Employee",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Positions_PositionId",
                table: "Employee",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceEmployees_Devices_DeviceId",
                table: "DeviceEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_DeviceEmployees_Employee_EmployeeId",
                table: "DeviceEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_DeviceTypes_DeviceTypeId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Persons_PersonId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Positions_PositionId",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Positions",
                table: "Positions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Persons",
                table: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeviceTypes",
                table: "DeviceTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Devices",
                table: "Devices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeviceEmployees",
                table: "DeviceEmployees");

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DeviceEmployees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DeviceEmployees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DeviceEmployees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DeviceEmployees",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DeviceEmployees",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DeviceTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DeviceTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DeviceTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DeviceTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DeviceTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.RenameTable(
                name: "Positions",
                newName: "Position");

            migrationBuilder.RenameTable(
                name: "Persons",
                newName: "Person");

            migrationBuilder.RenameTable(
                name: "DeviceTypes",
                newName: "DeviceType");

            migrationBuilder.RenameTable(
                name: "Devices",
                newName: "Device");

            migrationBuilder.RenameTable(
                name: "DeviceEmployees",
                newName: "DeviceEmployee");

            migrationBuilder.RenameIndex(
                name: "IX_Devices_DeviceTypeId",
                table: "Device",
                newName: "IX_Device_DeviceTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_DeviceEmployees_EmployeeId",
                table: "DeviceEmployee",
                newName: "IX_DeviceEmployee_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_DeviceEmployees_DeviceId",
                table: "DeviceEmployee",
                newName: "IX_DeviceEmployee_DeviceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Position",
                table: "Position",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Person",
                table: "Person",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeviceType",
                table: "DeviceType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Device",
                table: "Device",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeviceEmployee",
                table: "DeviceEmployee",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Device_DeviceType_DeviceTypeId",
                table: "Device",
                column: "DeviceTypeId",
                principalTable: "DeviceType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceEmployee_Device_DeviceId",
                table: "DeviceEmployee",
                column: "DeviceId",
                principalTable: "Device",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceEmployee_Employee_EmployeeId",
                table: "DeviceEmployee",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Person_PersonId",
                table: "Employee",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Position_PositionId",
                table: "Employee",
                column: "PositionId",
                principalTable: "Position",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
