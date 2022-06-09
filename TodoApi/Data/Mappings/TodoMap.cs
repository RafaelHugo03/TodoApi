using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApi.Entities;

namespace TodoApi.Data.Mappings
{
    public class TodoMap : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.ToTable("Todo");
            builder.HasKey(t => t.Id);

            builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd();

            builder.Property(x => x.Done)
                .HasColumnName("Done")
                .HasColumnType("BIT");

            builder.Property(x => x.Date)
                .HasColumnName("Date")
                .HasColumnType("SMALLDATETIME");

            builder.Property(x => x.UserId).HasColumnName("UserId");

        }
    }
}
