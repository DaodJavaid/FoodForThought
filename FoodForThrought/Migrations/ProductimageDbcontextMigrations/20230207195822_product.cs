using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodForThrought.Migrations.ProductimageDbcontextMigrations
{
    /// <inheritdoc />
    public partial class product : Migration
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
                    producttitle = table.Column<string>(name: "product_title", type: "nvarchar(max)", nullable: false),
                    productdesription = table.Column<string>(name: "product_desription", type: "nvarchar(max)", nullable: false),
                    productimg = table.Column<string>(name: "product_img", type: "nvarchar(max)", nullable: false),
                    productimagename = table.Column<string>(name: "product_image_name", type: "nvarchar(max)", nullable: false)
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
