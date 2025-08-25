using CheckInSimulation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CheckInSimulation.Data
{
    public class AirlineDbContext: DbContext
    {
        public AirlineDbContext(DbContextOptions<AirlineDbContext> options) : base(options)
        {

        }

        public DbSet<Airplane> Airplanes { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<SeatType> SeatTypes { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<BoardingPass> BoardingPasses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Airplane>().ToTable("airplane");
            modelBuilder.Entity<Flight>().ToTable("flight");
            modelBuilder.Entity<Passenger>().ToTable("passenger");
            modelBuilder.Entity<Purchase>().ToTable("purchase");
            modelBuilder.Entity<SeatType>().ToTable("seat_type");
            modelBuilder.Entity<Seat>().ToTable("seat");
            modelBuilder.Entity<BoardingPass>().ToTable("boarding_pass");
            /* Convierte snake_case a PascalCase automáticamente
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName()?.ToLower());
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.GetColumnName(StoreObjectIdentifier.Table(name: entity.GetTableName(), schema: null))?.ToLower());
                }
            }
            */
            /* Configuraciones de las entidades y relaciones 
            base.OnModelCreating(modelBuilder);
            // Configuraciones adicionales si es necesario
            modelBuilder.Entity<Airplane>()
                .HasMany(a => a.Flights)
                .WithOne(f => f.Airplane)
                .HasForeignKey(f => f.AirplaneId);
            modelBuilder.Entity<Airplane>()
                .HasMany(a => a.Seats)
                .WithOne(s => s.Airplane)
                .HasForeignKey(s => s.AirplaneId);
            modelBuilder.Entity<SeatType>()
                .HasMany(st => st.Seats)
                .WithOne(s => s.SeatType)
                .HasForeignKey(s => s.SeatTypeId);
            modelBuilder.Entity<SeatType>()
                .HasMany(st => st.BoardingPasses)
                .WithOne(bp => bp.SeatType)
                .HasForeignKey(bp => bp.SeatTypeId);
            modelBuilder.Entity<Seat>()
                .HasMany(s => s.BoardingPasses)
                .WithOne(bp => bp.Seat)
                .HasForeignKey(bp => bp.SeatId);
            modelBuilder.Entity<Passenger>()
                .HasMany(p => p.BoardingPasses)
                .WithOne(bp => bp.Passenger)
                .HasForeignKey(bp => bp.PassengerId);
            modelBuilder.Entity<Purchase>()
                .HasMany(pu => pu.BoardingPasses)
                .WithOne(bp => bp.Purchase)
                .HasForeignKey(bp => bp.PurchaseId);
            modelBuilder.Entity<Flight>()
                .HasMany(f => f.BoardingPasses)
                .WithOne(bp => bp.Flight)
                .HasForeignKey(bp => bp.FlightId);
            */
        }
    }
}
