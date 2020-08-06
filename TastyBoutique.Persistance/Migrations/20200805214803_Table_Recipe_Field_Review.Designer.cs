﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Persistance.Migrations
{
    [DbContext(typeof(TastyBoutiqueContext))]
    [Migration("20200805214803_Table_Recipe_Field_Review")]
    partial class Table_Recipe_Field_Review
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TastyBoutique.Persistance.Models.Filters", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Filters");
                });

            modelBuilder.Entity("TastyBoutique.Persistance.Models.Ingredients", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("TastyBoutique.Persistance.Models.RecipeComment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<Guid>("IdRecipe")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Review")
                        .HasColumnType("int")
                        .IsFixedLength(true)
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.HasIndex("IdRecipe");

                    b.HasIndex("IdUser");

                    b.ToTable("RecipeComments");
                });

            modelBuilder.Entity("TastyBoutique.Persistance.Models.RecipeType", b =>
                {
                    b.Property<Guid>("RecipeId")
                        .HasColumnName("RecipeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("RecipeId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeType");
                });

            modelBuilder.Entity("TastyBoutique.Persistance.Models.Recipes", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Access")
                        .HasColumnType("bit")
                        .HasMaxLength(25);

                    b.Property<float>("AverageReview")
                        .HasColumnType("real");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<Guid>("IdUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("ReviewCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdUser");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("TastyBoutique.Persistance.Models.RecipesFilters", b =>
                {
                    b.Property<Guid>("RecipeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FilterId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RecipeId", "FilterId");

                    b.HasIndex("FilterId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipesFilters");
                });

            modelBuilder.Entity("TastyBoutique.Persistance.Models.RecipesIngredients", b =>
                {
                    b.Property<Guid>("RecipeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IngredientId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RecipeId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipesIngredients");
                });

            modelBuilder.Entity("TastyBoutique.Persistance.Models.SavedRecipes", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdRecipe")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("NeedUpdate")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("IdRecipe");

                    b.HasIndex("IdUser");

                    b.ToTable("SavedRecipes");
                });

            modelBuilder.Entity("TastyBoutique.Persistance.Models.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Age")
                        .HasColumnType("numeric(2, 0)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("TastyBoutique.Persistance.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<Guid>("IdStudent")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdUserType")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("IdStudent");

                    b.HasIndex("IdUserType");

                    b.ToTable("User");
                });

            modelBuilder.Entity("TastyBoutique.Persistance.Models.UserType", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("UserType");
                });

            modelBuilder.Entity("TastyBoutique.Persistance.Models.RecipeComment", b =>
                {
                    b.HasOne("TastyBoutique.Persistance.Models.Recipes", "IdRecipeNavigation")
                        .WithMany("RecipeComment")
                        .HasForeignKey("IdRecipe")
                        .HasConstraintName("FK_RecipeComment_Recipes")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TastyBoutique.Persistance.Models.User", "IdUserNavigation")
                        .WithMany("RecipeComment")
                        .HasForeignKey("IdUser")
                        .HasConstraintName("FK_RecipeComment_User")
                        .IsRequired();
                });

            modelBuilder.Entity("TastyBoutique.Persistance.Models.RecipeType", b =>
                {
                    b.HasOne("TastyBoutique.Persistance.Models.Recipes", "Recipe")
                        .WithOne("RecipeType")
                        .HasForeignKey("TastyBoutique.Persistance.Models.RecipeType", "RecipeId")
                        .HasConstraintName("FK_RecipeType_Recipes")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TastyBoutique.Persistance.Models.RecipesFilters", b =>
                {
                    b.HasOne("TastyBoutique.Persistance.Models.Filters", "Filter")
                        .WithMany("RecipesFilters")
                        .HasForeignKey("FilterId")
                        .HasConstraintName("FK_RecipesFilters_Filters")
                        .IsRequired();

                    b.HasOne("TastyBoutique.Persistance.Models.Recipes", "Recipe")
                        .WithMany("RecipesFilters")
                        .HasForeignKey("RecipeId")
                        .HasConstraintName("FK_RecipesFilters_Recipes")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TastyBoutique.Persistance.Models.RecipesIngredients", b =>
                {
                    b.HasOne("TastyBoutique.Persistance.Models.Ingredients", "Ingredient")
                        .WithMany("RecipesIngredients")
                        .HasForeignKey("IngredientId")
                        .HasConstraintName("FK_RecipesIngredients_Ingredients")
                        .IsRequired();

                    b.HasOne("TastyBoutique.Persistance.Models.Recipes", "Recipe")
                        .WithMany("RecipesIngredients")
                        .HasForeignKey("RecipeId")
                        .HasConstraintName("FK_RecipesIngredients_Recipes")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TastyBoutique.Persistance.Models.SavedRecipes", b =>
                {
                    b.HasOne("TastyBoutique.Persistance.Models.Recipes", "IdRecipeNavigation")
                        .WithMany("SavedRecipes")
                        .HasForeignKey("IdRecipe")
                        .HasConstraintName("FK_SavedRecipes_Recipes")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TastyBoutique.Persistance.Models.User", "IdUserNavigation")
                        .WithMany("SavedRecipes")
                        .HasForeignKey("IdUser")
                        .HasConstraintName("FK_SavedRecipes_User")
                        .IsRequired();
                });

            modelBuilder.Entity("TastyBoutique.Persistance.Models.User", b =>
                {
                    b.HasOne("TastyBoutique.Persistance.Models.Student", "IdStudentNavigation")
                        .WithMany("User")
                        .HasForeignKey("IdStudent")
                        .HasConstraintName("FK_User_Student")
                        .IsRequired();

                    b.HasOne("TastyBoutique.Persistance.Models.UserType", "IdUserTypeNavigation")
                        .WithMany("User")
                        .HasForeignKey("IdUserType")
                        .HasConstraintName("FK_User_UserType")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
