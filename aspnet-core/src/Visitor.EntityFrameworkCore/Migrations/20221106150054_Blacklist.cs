using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Visitor.Migrations
{
    public partial class Blacklist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Remarks",
                table: "Blacklist",
                newName: "BlacklistRemarks");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Blacklist",
                newName: "BlacklistPhoneNumber");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Blacklist",
                newName: "BlacklistIdentityCard");

            migrationBuilder.AddColumn<string>(
                name: "BlacklistFullName",
                table: "Blacklist",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlacklistFullName",
                table: "Blacklist");

            migrationBuilder.RenameColumn(
                name: "BlacklistRemarks",
                table: "Blacklist",
                newName: "Remarks");

            migrationBuilder.RenameColumn(
                name: "BlacklistPhoneNumber",
                table: "Blacklist",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "BlacklistIdentityCard",
                table: "Blacklist",
                newName: "FullName");
        }
    }
}
