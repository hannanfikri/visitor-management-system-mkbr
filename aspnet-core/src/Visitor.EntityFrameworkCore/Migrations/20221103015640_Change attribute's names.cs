using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Visitor.Migrations
{
    public partial class Changeattributesnames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "companies",
                newName: "CompanyEmail");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "companies",
                newName: "CompanyAddress");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompanyEmail",
                table: "companies",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "CompanyAddress",
                table: "companies",
                newName: "Address");
        }
    }
}
