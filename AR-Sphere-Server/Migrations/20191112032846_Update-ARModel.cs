using Microsoft.EntityFrameworkCore.Migrations;

namespace ARSphere.Migrations
{
    public partial class UpdateARModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "ARModels",
                newName: "FileName");

            migrationBuilder.AddColumn<int>(
                name: "Cost",
                table: "ARModels",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "ARModels");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "ARModels",
                newName: "Url");
        }
    }
}
