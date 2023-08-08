using _net_angular_demo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace _net_angular_demo.Data;


public class Entities: DbContext{

    public DbSet<Passenger> passengers => Set<Passenger>();

    public DbSet<Flight> flights => Set<Flight>();

    public Entities(DbContextOptions<Entities> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Passenger>().HasKey(p => p.Email);

        modelBuilder.Entity<Flight>().Property(p => p.remainingSeats).IsConcurrencyToken();

        modelBuilder.Entity<Flight>().OwnsOne(f => f.Departure);
        modelBuilder.Entity<Flight>().OwnsOne(f => f.Arrival);
    }
}