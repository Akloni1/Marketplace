﻿// <auto-generated />
using Marketplace.Services.ProductAPI.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Marketplace.Services.ProductAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240405151631_SeedProducts")]
    partial class SeedProducts
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Marketplace.Services.ProductAPI.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryName = "Техника",
                            Description = "Мужские часы кварцевые.",
                            ImageUrl = "C:\\Users\\Михаил\\Desktop\\Marketplace\\Image\\screw.jpeg",
                            Name = "Мужские часы",
                            Price = 15000.0
                        },
                        new
                        {
                            Id = 2,
                            CategoryName = "Техника",
                            Description = "IPhone 11.",
                            ImageUrl = "C:\\Users\\Михаил\\Desktop\\Marketplace\\Image\\screw.jpeg",
                            Name = "IPhone 11",
                            Price = 60000.0
                        },
                        new
                        {
                            Id = 3,
                            CategoryName = "Одежда",
                            Description = "Футболка размер M",
                            ImageUrl = "C:\\Users\\Михаил\\Desktop\\Marketplace\\Image\\screw.jpeg",
                            Name = "Футболка",
                            Price = 4000.0
                        },
                        new
                        {
                            Id = 4,
                            CategoryName = "Ремонт",
                            Description = "Шуруп-глухарь 12х180 мм для крепления деревянных лаг и реек. Цена указана за 8 шт.",
                            ImageUrl = "C:\\Users\\Михаил\\Desktop\\Marketplace\\Image\\screw.jpeg",
                            Name = "Шуруп-глухарь",
                            Price = 150.0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
