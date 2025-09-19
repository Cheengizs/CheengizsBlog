using AccessDb.Entities;
using Core.Enums;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccessDb.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Name).IsRequired().HasMaxLength(User.MAX_USERNAME_LENGTH);
     
        builder.Property(o => o.Role)
            .HasConversion<string>()
            .HasDefaultValue(Roles.defUser)
            .IsRequired();

        builder.Property(o => o.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.HasMany(u => u.Comments).WithOne(c => c.User);
     
        builder.HasMany(u => u.LikedPosts).WithMany(p => p.UsersLiked);
        
        builder.HasMany(u => u.LikedComments).WithMany(c => c.LikedUsers);
    }
}