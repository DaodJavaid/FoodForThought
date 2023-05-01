using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodForThrought.Migrations
{
    /// <inheritdoc />
    public partial class Signuping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Signup",
                table: "Signup");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "Signup",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Signup",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Signup",
                table: "Signup",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Signup",
                table: "Signup");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Signup");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "Signup",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Signup",
                table: "Signup",
                column: "username");
        }
    }
}
