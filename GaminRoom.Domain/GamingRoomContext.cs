using GaminRoom.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace GaminRoom.Domain
{
    public class GamingRoomContext : DbContext
    {
        public GamingRoomContext(DbContextOptions<GamingRoomContext> options)
              :base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transfer>()
                .HasOne(u => u.Sender)
                .WithMany(t => t.Outgoing)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transfer>()
                .HasOne(u => u.Receiver)
                .WithMany(t => t.Incoming)
                .OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Code> Codes { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
    }
}
