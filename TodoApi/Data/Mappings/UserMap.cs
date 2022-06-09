using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApi.Entities;

namespace TodoApi.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
           
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasColumnName("FirstName")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50);
            builder.Property(x => x.LastName)
                .IsRequired()
                .HasColumnName("LastName")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50);

            builder.Property(x => x.Password)
               .IsRequired()
               .HasColumnName("Password")
               .HasColumnType("NVARCHAR")
               .HasMaxLength(255);


            builder.HasMany(x => x.Todos)
                .WithOne()
                .HasForeignKey(x => x.UserId);

            builder.HasIndex(x => x.Email, "IX_User_Email").IsUnique();

            builder.HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity<Dictionary<string, Object>>(
                "UserRole",
                role => role.HasOne<Role>()
                .WithMany()
                .HasForeignKey("RoleId")
                .HasConstraintName("FK_UserRole_RoleId")
                .OnDelete(DeleteBehavior.Cascade),
                user => user.HasOne<User>()
                .WithMany()
                .HasForeignKey("UserId")
                .HasConstraintName("FK_UserRole_UserId")
                .OnDelete(DeleteBehavior.Cascade));

        }
    }
}
