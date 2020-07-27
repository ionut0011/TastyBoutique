using Microsoft.EntityFrameworkCore;
using System;

namespace TastyBoutique.Persistance.Models
{
    public partial class TastyBoutique_v2Context : DbContext
    {
        public TastyBoutique_v2Context()
        {
        }

        public TastyBoutique_v2Context(DbContextOptions<TastyBoutique_v2Context> options)
            : base(options)
        {
            Database.Migrate();
        }

        public virtual DbSet<Filters> Filters { get; set; }
        public virtual DbSet<Ingredients> Ingredients { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }
        public virtual DbSet<RecipeComment> RecipeComment { get; set; }
        public virtual DbSet<RecipeType> RecipeType { get; set; }
        public virtual DbSet<Recipes> Recipes { get; set; }
        public virtual DbSet<RecipesFilters> RecipesFilters { get; set; }
        public virtual DbSet<RecipesIngredients> RecipesIngredients { get; set; }
        public virtual DbSet<SavedRecipes> SavedRecipes { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLExpress;Database=TastyBoutique_v3;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Filters>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Ingredients>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Notifications>(entity =>
            {
                entity.HasIndex(e => e.IdRecipe);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdRecipeNavigation)
                    .WithMany(p => p.NotificationsNavigation)
                    .HasForeignKey(d => d.IdRecipe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notifications_Recipes");
            });

            modelBuilder.Entity<RecipeComment>(entity =>
            {
                entity.HasIndex(e => e.IdRecipe);

                entity.HasIndex(e => e.IdUser);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Review)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.IdRecipeNavigation)
                    .WithMany(p => p.RecipeComment)
                    .HasForeignKey(d => d.IdRecipe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RecipeComment_Recipes");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.RecipeComment)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RecipeComment_User");
            });

            modelBuilder.Entity<RecipeType>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.RecipeId);

                entity.Property(e => e.RecipeId).HasColumnName("RecipeID");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Recipe)
                    .WithMany()
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RecipeType_Recipes");
            });

            modelBuilder.Entity<Recipes>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Access)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.Image).HasColumnType("image");

                entity.Property(e => e.Link).HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Notifications).HasMaxLength(250);
            });

            modelBuilder.Entity<RecipesFilters>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.FilterId);

                entity.HasIndex(e => e.RecipeId);

                entity.HasOne(d => d.Filter)
                    .WithMany()
                    .HasForeignKey(d => d.FilterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RecipesFilters_Filters");

                entity.HasOne(d => d.Recipe)
                    .WithMany()
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RecipesFilters_Recipes");
            });

            modelBuilder.Entity<RecipesIngredients>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.IngredientId);

                entity.HasIndex(e => e.RecipeId);

                entity.HasOne(d => d.Ingredient)
                    .WithMany()
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RecipesIngredients_Ingredients");

                entity.HasOne(d => d.Recipe)
                    .WithMany()
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RecipesIngredients_Recipes");
            });

            modelBuilder.Entity<SavedRecipes>(entity =>
            {
                entity.HasIndex(e => e.IdRecipe);

                entity.HasIndex(e => e.IdUser);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.IdRecipeNavigation)
                    .WithMany(p => p.SavedRecipes)
                    .HasForeignKey(d => d.IdRecipe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SavedRecipes_Recipes");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.SavedRecipes)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SavedRecipes_User");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Age).HasColumnType("numeric(2, 0)");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.IdStudent);

                entity.HasIndex(e => e.IdUserType);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(250);

                entity.Property(e => e.Status)
                    .HasMaxLength(50);
                    
                    

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdStudentNavigation)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.IdStudent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Student");

                entity.HasOne(d => d.IdUserTypeNavigation)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.IdUserType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_UserType");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
