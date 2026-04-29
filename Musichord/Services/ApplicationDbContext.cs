using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Musichord.Models.Entities;

namespace Musichord.Services;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>()
        .HasIndex(u => u.Handle)
        .IsUnique();
    }

    public DbSet<Track> Tracks => Set<Track>();
    public DbSet<Artist> Artists => Set<Artist>();
    public DbSet<Friendship> Friendships => Set<Friendship>();
    public DbSet<FavoriteTrack> FavoriteTracks => Set<FavoriteTrack>();
    public DbSet<ListenRecord> ListenRecords => Set<ListenRecord>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Album> Albums => Set<Album>();
}