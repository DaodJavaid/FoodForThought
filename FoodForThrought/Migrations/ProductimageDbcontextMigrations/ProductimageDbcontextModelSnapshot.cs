﻿// <auto-generated />
using FoodForThrought.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FoodForThrought.Migrations.ProductimageDbcontextMigrations
{
    [DbContext(typeof(ProductimageDbcontext))]
    partial class ProductimageDbcontextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FoodForThrought.Models.AddingProductModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("product_desription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("product_image_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("product_img")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("product_title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AddingProduct");
                });
#pragma warning restore 612, 618
        }
    }
}
