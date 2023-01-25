using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Visitor.Migrations
{
    public partial class Addseedtower : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Towers",
                columns: new[] { "Id", "TowerBankRakyat" },
                values: new object[] { new Guid("056c7b57-9c59-4fb3-b919-1d242a32f8af"), "Tower 2" });

            migrationBuilder.InsertData(
                table: "Towers",
                columns: new[] { "Id", "TowerBankRakyat" },
                values: new object[] { new Guid("39ec162d-ddf6-47a8-9e1e-ea7abac9cdd4"), "Tower 1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Towers",
                keyColumn: "Id",
                keyValue: new Guid("056c7b57-9c59-4fb3-b919-1d242a32f8af"));

            migrationBuilder.DeleteData(
                table: "Towers",
                keyColumn: "Id",
                keyValue: new Guid("39ec162d-ddf6-47a8-9e1e-ea7abac9cdd4"));
        }
    }
}
