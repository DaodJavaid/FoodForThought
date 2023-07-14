using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodForThrought.Migrations.QuestionnaireDb
{
    /// <inheritdoc />
    public partial class Questions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    emotion_questtion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    first_option = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    second_option = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    third_option = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    forth_option = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    select_emotion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Question");
        }
    }
}
