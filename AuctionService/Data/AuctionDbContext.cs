using AuctionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Data;

public class AuctionDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Auction> Auctions { get; set; }
    public DbSet<Item> Items { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Auction>()
            .HasOne(auction => auction.Item)
            .WithOne(item => item.Auction)
            .HasForeignKey<Item>(x => x.Id)
            .IsRequired();
        
        modelBuilder.Entity<Item>()
            .ToTable("Items");
    }
}