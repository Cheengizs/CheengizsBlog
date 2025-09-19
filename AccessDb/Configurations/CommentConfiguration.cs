using AccessDb.Entities;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccessDb.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<CommentEntity>
{
    public void Configure(EntityTypeBuilder<CommentEntity> builder)
    {
        builder.HasKey(с => с.Id);

        builder.Property(c => c.Content).IsRequired().HasMaxLength(Comment.MaxCommentLength);
     
        builder.Property(o => o.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(o => o.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired()
            .ValueGeneratedOnAdd()
            .ValueGeneratedOnUpdate();

        builder.Property(c => c.Rating).HasDefaultValue(0);
        
        builder.HasMany(c => c.LikedUsers).WithMany(u => u.LikedComments);
     
        builder.HasOne(c => c.Post).WithMany(p => p.Comments);

        builder.HasOne(c => c.User).WithMany(u => u.Comments);
        
        
    }
}