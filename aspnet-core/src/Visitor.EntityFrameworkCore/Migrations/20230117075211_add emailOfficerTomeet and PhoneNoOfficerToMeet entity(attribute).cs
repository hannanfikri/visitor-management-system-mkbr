using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Visitor.Migrations
{
    public partial class addemailOfficerTomeetandPhoneNoOfficerToMeetentityattribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "passNumber",
                table: "Appointments",
                newName: "PassNumber");

            migrationBuilder.AddColumn<string>(
                name: "EmailOfficerToMeet",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNoOfficerToMeet",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailOfficerToMeet",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PhoneNoOfficerToMeet",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "PassNumber",
                table: "Appointments",
                newName: "passNumber");
        }
    }
}
