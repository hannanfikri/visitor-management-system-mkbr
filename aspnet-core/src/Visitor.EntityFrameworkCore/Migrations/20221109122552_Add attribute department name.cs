using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Visitor.Migrations
{
    public partial class Addattributedepartmentname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Departments_AppointmentEnt",
                table: "Departments");

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_AppointmentEnt",
                table: "Departments",
                column: "AppointmentEnt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Departments_AppointmentEnt",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "Appointments");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_AppointmentEnt",
                table: "Departments",
                column: "AppointmentEnt",
                unique: true,
                filter: "[AppointmentEnt] IS NOT NULL");
        }
    }
}
