using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Visitor.Migrations
{
    public partial class addCancelDateTimeinappointmenttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "passNumber",
                table: "Appointments",
                newName: "PassNumber");

            migrationBuilder.AddColumn<DateTime>(
                name: "CancelDateTime",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancelDateTime",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "PassNumber",
                table: "Appointments",
                newName: "passNumber");
        }
    }
}
