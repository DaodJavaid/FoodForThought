using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodForThrought.Migrations.ContactDbcontextMigrations
{
    /// <inheritdoc />
    public partial class messagedatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    useremail = table.Column<string>(name: "user_email", type: "nvarchar(450)", nullable: false),
                    username = table.Column<string>(name: "user_name", type: "nvarchar(max)", nullable: false),
                    usermessage = table.Column<string>(name: "user_message", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.useremail);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Message");
        }
    }
}
