using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Visitor.Migrations
{
    public partial class AddImageIdColumninAppointmentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FaceVerify",
                table: "Appointments");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Appointments");

            migrationBuilder.AddColumn<byte[]>(
                name: "FaceVerify",
                table: "Appointments",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
