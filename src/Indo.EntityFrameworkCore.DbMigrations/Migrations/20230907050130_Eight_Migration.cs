using Microsoft.EntityFrameworkCore.Migrations;

namespace Indo.Migrations
{
    public partial class Eight_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DesignationId",
                table: "AppEmployees");

            migrationBuilder.AlterColumn<int>(
                name: "ActiveStatus",
                table: "AppEmployees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Designation",
                table: "AppEmployees",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Designation",
                table: "AppEmployees");

            migrationBuilder.AlterColumn<string>(
                name: "ActiveStatus",
                table: "AppEmployees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "DesignationId",
                table: "AppEmployees",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
