using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Visitor.Migrations
{
    public partial class addattributepassnumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {/*
            migrationBuilder.DropColumn(
                name: "RegDateTime",
                table: "Appointments");*/

            migrationBuilder.AddColumn<string>(
                name: "passNumber",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "passNumber",
                table: "Appointments");
/*
            migrationBuilder.AddColumn<DateTime>(
                name: "RegDateTime",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));*/
        }
    }
}
