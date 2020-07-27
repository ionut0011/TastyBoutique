﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TastyBoutique.Persistance.Models;

namespace TastyBoutique.Persistance.Migrations
{
    [DbContext(typeof(TastyBoutique_v2Context))]
    partial class TastyBoutique_v2ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("TastyBoutique.Persistance.Models.Notifications", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<Guid>("IdRecipe")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("IdRecipe");

                    b.ToTable("Notifications");
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

                    b.Property<string>("Review")
                        .IsRequired()
                        .HasColumnType("nchar(10)")
                        .IsFixedLength(true)
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.HasIndex("IdRecipe");

                    b.HasIndex("IdUser");

                    b.ToTable("RecipeComment");
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

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeType");
                });

            modelBuilder.Entity("TastyBoutique.Persistance.Models.Recipes", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Access")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<byte[]>("Image")
                        .HasColumnType("image");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Notifications")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("TastyBoutique.Persistance.Models.RecipesFilters", b =>
                {
                    b.Property<Guid>("FilterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RecipeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("FilterId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipesFilters");
                });

            modelBuilder.Entity("TastyBoutique.Persistance.Models.RecipesIngredients", b =>
                {
                    b.Property<Guid>("IngredientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RecipeId")
                        .HasColumnType("uniqueidentifier");

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

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("UserType");
                });

            modelBuilder.Entity("TastyBoutique.Persistance.Models.Notifications", b =>
                {
                    b.HasOne("TastyBoutique.Persistance.Models.Recipes", "IdRecipeNavigation")
                        .WithMany("NotificationsNavigation")
                        .HasForeignKey("IdRecipe")
                        .HasConstraintName("FK_Notifications_Recipes")
                        .IsRequired();
                });

            modelBuilder.Entity("TastyBoutique.Persistance.Models.RecipeComment", b =>
                {
                    b.HasOne("TastyBoutique.Persistance.Models.Recipes", "IdRecipeNavigation")
                        .WithMany("RecipeComment")
                        .HasForeignKey("IdRecipe")
                        .HasConstraintName("FK_RecipeComment_Recipes")
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
                        .WithMany()
                        .HasForeignKey("RecipeId")
                        .HasConstraintName("FK_RecipeType_Recipes")
                        .IsRequired();
                });

            modelBuilder.Entity("TastyBoutique.Persistance.Models.RecipesFilters", b =>
                {
                    b.HasOne("TastyBoutique.Persistance.Models.Filters", "Filter")
                        .WithMany()
                        .HasForeignKey("FilterId")
                        .HasConstraintName("FK_RecipesFilters_Filters")
                        .IsRequired();

                    b.HasOne("TastyBoutique.Persistance.Models.Recipes", "Recipe")
                        .WithMany()
                        .HasForeignKey("RecipeId")
                        .HasConstraintName("FK_RecipesFilters_Recipes")
                        .IsRequired();
                });

            modelBuilder.Entity("TastyBoutique.Persistance.Models.RecipesIngredients", b =>
                {
                    b.HasOne("TastyBoutique.Persistance.Models.Ingredients", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .HasConstraintName("FK_RecipesIngredients_Ingredients")
                        .IsRequired();

                    b.HasOne("TastyBoutique.Persistance.Models.Recipes", "Recipe")
                        .WithMany()
                        .HasForeignKey("RecipeId")
                        .HasConstraintName("FK_RecipesIngredients_Recipes")
                        .IsRequired();
                });

            modelBuilder.Entity("TastyBoutique.Persistance.Models.SavedRecipes", b =>
                {
                    b.HasOne("TastyBoutique.Persistance.Models.Recipes", "IdRecipeNavigation")
                        .WithMany("SavedRecipes")
                        .HasForeignKey("IdRecipe")
                        .HasConstraintName("FK_SavedRecipes_Recipes")
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
