using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EFRelationships.Models;


namespace EFRelationships;

public class MyDbContext : DbContext
{
    private string _connectionString;

    public DbSet<User> Users { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Game> Games { get; set; }


    public MyDbContext(IConfiguration configuration) : base()
    {
        _connectionString = configuration["ConnectionString"]!;
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasMany(u => u.Games).WithMany(g => g.Users);
            entity.ToTable("Users");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.HasOne(x => x.Game).WithMany(g => g.Comments);
            entity.ToTable("Comments");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(entity => entity.Id);
            entity.HasMany(u => u.Users).WithMany(g => g.Games);
            entity.HasMany(g => g.Comments).WithOne(g => g.Game);
            entity.ToTable("Games");
        });

        base.OnModelCreating(modelBuilder);
    }
}
