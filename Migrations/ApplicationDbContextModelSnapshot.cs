﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShoppingListDemo.Data;

#nullable disable

namespace ShoppingListDemo.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ShoppingItemShoppingSchedule", b =>
                {
                    b.Property<int>("ShoppingItemsId")
                        .HasColumnType("int");

                    b.Property<int>("ShoppingSchedulesId")
                        .HasColumnType("int");

                    b.HasKey("ShoppingItemsId", "ShoppingSchedulesId");

                    b.HasIndex("ShoppingSchedulesId");

                    b.ToTable("ShoppingItemShoppingSchedule");
                });

            modelBuilder.Entity("ShoppingListDemo.Data.ShoppingCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("ShoppingCategories");
                });

            modelBuilder.Entity("ShoppingListDemo.Data.ShoppingItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(65)
                        .HasColumnType("varchar(65)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(8,2)");

                    b.Property<int>("ShoppingCategoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ShoppingCategoryId");

                    b.ToTable("ShoppingItems");
                });

            modelBuilder.Entity("ShoppingListDemo.Data.ShoppingSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Day")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("Day")
                        .IsUnique();

                    b.ToTable("ShoppingSchedules");
                });

            modelBuilder.Entity("ShoppingItemShoppingSchedule", b =>
                {
                    b.HasOne("ShoppingListDemo.Data.ShoppingItem", null)
                        .WithMany()
                        .HasForeignKey("ShoppingItemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShoppingListDemo.Data.ShoppingSchedule", null)
                        .WithMany()
                        .HasForeignKey("ShoppingSchedulesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ShoppingListDemo.Data.ShoppingItem", b =>
                {
                    b.HasOne("ShoppingListDemo.Data.ShoppingCategory", "ShoppingCategory")
                        .WithMany("ShoppingItems")
                        .HasForeignKey("ShoppingCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShoppingCategory");
                });

            modelBuilder.Entity("ShoppingListDemo.Data.ShoppingCategory", b =>
                {
                    b.Navigation("ShoppingItems");
                });
#pragma warning restore 612, 618
        }
    }
}
