using Microsoft.EntityFrameworkCore.Migrations;

namespace crm.Migrations
{
    public partial class migracja2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IDX_symbol_unique",
                table: "Currency");

            migrationBuilder.AlterColumn<string>(
                name: "Symbol",
                table: "Currency",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Currency",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IDX_symbol_unique",
                table: "Currency",
                column: "Symbol",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IDX_symbol_unique",
                table: "Currency");

            migrationBuilder.AlterColumn<string>(
                name: "Symbol",
                table: "Currency",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Currency",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IDX_symbol_unique",
                table: "Currency",
                column: "Symbol",
                unique: true,
                filter: "[Symbol] IS NOT NULL");
        }
    }
}
