using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodForThrought.Migrations.ProductimageDbcontextMigrations
{
    /// <inheritdoc />
    public partial class productimage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddingProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    product_desription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    product_img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    product_image_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddingProduct", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddingProduct");
        }
    }
}
