﻿// <auto-generated />
using FoodForThrought.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FoodForThrought.Migrations.ProductimageDbcontextMigrations
{
    [DbContext(typeof(ProductimageDbcontext))]
    [Migration("20230205230225_productdata")]
    partial class productdata
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FoodForThrought.Models.AddingProductModel", b =>
                {
                    b.Property<string>("product_title")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("product_desription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("product_img")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("product_title");

                    b.ToTable("ProductImages");
                });
#pragma warning restore 612, 618
        }
    }
}
