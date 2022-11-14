using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Visitor.Migrations
{
    public partial class AppointmnetIhasCreationTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegDateTime",
                table: "Appointments",
                newName: "CreationTime");

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "Appointments",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "Appointments",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Appointments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Appointments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "Appointments",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "CreationTime",
                table: "Appointments",
                newName: "RegDateTime");
        }
    }
}
