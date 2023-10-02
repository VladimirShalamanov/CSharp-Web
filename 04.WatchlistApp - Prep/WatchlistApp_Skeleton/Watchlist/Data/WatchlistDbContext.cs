﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Watchlist.Data.Models;

namespace Watchlist.Data
{
    public class WatchlistDbContext : IdentityDbContext<User>
    {
        public WatchlistDbContext(DbContextOptions<WatchlistDbContext> options)
            : base(options)
        {
        }

        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<Movie> Movies { get; set; } = null!;
        public DbSet<UserMovie> UserMovies { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<UserMovie>()
                .HasKey(k => new { k.UserId, k.MovieId });

            builder
                .Entity<User>()
                .Property(u => u.UserName)
                .HasMaxLength(20)
                .IsRequired();

            builder
                .Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(60)
                .IsRequired();

            builder
                .Entity<Genre>()
                .HasData(new Genre()
                {
                    Id = 1,
                    Name = "Action"
                },
                new Genre()
                {
                    Id = 2,
                    Name = "Comedy"
                },
                new Genre()
                {
                    Id = 3,
                    Name = "Drama"
                },
                new Genre()
                {
                    Id = 4,
                    Name = "Horror"
                },
                new Genre()
                {
                    Id = 5,
                    Name = "Romantic"
                });

            base.OnModelCreating(builder);
        }
    }
}