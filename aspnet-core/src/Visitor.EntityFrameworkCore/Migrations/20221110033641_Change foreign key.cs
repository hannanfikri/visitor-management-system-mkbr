using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Visitor.Migrations
{
    public partial class Changeforeignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Appointments_AppointmentEnt",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_AppointmentEnt",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "AppointmentEnt",
                table: "Departments");

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Appointments");

            migrationBuilder.AddColumn<Guid>(
                name: "AppointmentEnt",
                table: "Departments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_AppointmentEnt",
                table: "Departments",
                column: "AppointmentEnt");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Appointments_AppointmentEnt",
                table: "Departments",
                column: "AppointmentEnt",
                principalTable: "Appointments",
                principalColumn: "Id");
        }
    }
}
