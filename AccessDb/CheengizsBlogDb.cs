using AccessDb.Configurations;
using Microsoft.EntityFrameworkCore;
using AccessDb.Entities;

namespace AccessDb;

public class CheengizsBlogDb : DbContext
{
    public CheengizsBlogDb(DbContextOptions<CheengizsBlogDb> options) : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<PostEntity> Posts { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new PostConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}