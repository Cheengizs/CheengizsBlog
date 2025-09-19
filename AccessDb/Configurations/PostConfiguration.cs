using AccessDb.Entities;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccessDb.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<PostEntity>
{
    public void Configure(EntityTypeBuilder<PostEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title).IsRequired().HasMaxLength(Post.MaxTitleLength);

        builder.Property(p => p.Content).IsRequired().HasMaxLength(Post.MaxPostLength);

        builder.Property(o => o.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(o => o.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired()
            .ValueGeneratedOnAdd()
            .ValueGeneratedOnUpdate();

        builder.Property(p => p.Rating).HasDefaultValue(0);
        
        builder.HasMany(p => p.Comments).WithOne(c => c.Post);

        builder.HasMany(p => p.UsersLiked).WithMany(u => u.LikedPosts);
    }
}