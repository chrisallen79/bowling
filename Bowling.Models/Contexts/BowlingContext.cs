using System;
using Microsoft.EntityFrameworkCore;

namespace Bowling.Models.Context
{
    public class BowlingContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Frame> Frames { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=BowlingDB.db;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>(entity =>
            {
                entity.Property(prop => prop.GameStarted)
                    .HasDefaultValueSql("date('now')");
            });

            modelBuilder.Entity<Game>().ToTable("Games");
            modelBuilder.Entity<Frame>().ToTable("Frames");

            // seed the database with a perfect game
            modelBuilder.Entity<Game>().HasData(new Game { GameId = 1 });
            modelBuilder.Entity<Frame>().HasData(
                new Frame { FrameId = 1, Roll1 = 10, GameId = 1 },
                new Frame { FrameId = 2, Roll1 = 10, GameId = 1 },
                new Frame { FrameId = 3, Roll1 = 10, GameId = 1 },
                new Frame { FrameId = 4, Roll1 = 10, GameId = 1 },
                new Frame { FrameId = 5, Roll1 = 10, GameId = 1 },
                new Frame { FrameId = 6, Roll1 = 10, GameId = 1 },
                new Frame { FrameId = 7, Roll1 = 10, GameId = 1 },
                new Frame { FrameId = 8, Roll1 = 10, GameId = 1 },
                new Frame { FrameId = 9, Roll1 = 10, GameId = 1 },
                new Frame { FrameId = 10, Roll1 = 10, Roll2 = 10, Roll3 = 10, GameId = 1 }
            );
        }
    }
}