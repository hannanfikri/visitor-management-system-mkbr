using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Visitor.Migrations
{
    public partial class Adddepartmentnameattribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Departments_DepartmentId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_DepartmentId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "Departments");

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
                column: "AppointmentEnt",
                unique: true,
                filter: "[AppointmentEnt] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Appointments_AppointmentEnt",
                table: "Departments",
                column: "AppointmentEnt",
                principalTable: "Appointments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "AppointmentId",
                table: "Departments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DepartmentId",
                table: "Appointments",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Departments_DepartmentId",
                table: "Appointments",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }
    }
}
