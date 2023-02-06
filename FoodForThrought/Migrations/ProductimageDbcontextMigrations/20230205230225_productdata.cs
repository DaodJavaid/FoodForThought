using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodForThrought.Migrations.ProductimageDbcontextMigrations
{
    /// <inheritdoc />
    public partial class productdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    producttitle = table.Column<string>(name: "product_title", type: "nvarchar(450)", nullable: false),
                    productdesription = table.Column<string>(name: "product_desription", type: "nvarchar(max)", nullable: false),
                    productimg = table.Column<string>(name: "product_img", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.producttitle);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");
        }
    }
}
