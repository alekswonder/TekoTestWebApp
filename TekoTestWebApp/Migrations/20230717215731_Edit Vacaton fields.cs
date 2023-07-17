using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TekoTestWebApp.Migrations
{
    public partial class EditVacatonfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Vacations",
                newName: "StartDateTime");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Vacations",
                newName: "EndDateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDateTime",
                table: "Vacations",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "EndDateTime",
                table: "Vacations",
                newName: "EndDate");
        }
    }
}
