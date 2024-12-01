using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection.Emit;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
{
    public required DbSet<Icon> Icons { get; set; }
    public required DbSet<Location> Locations { get; set; }
    public required DbSet<Stop> Stops { get; set; }
    public required DbSet<LocationStop> LocationStops { get; set; }
    public required DbSet<Line> Lines { get; set; }
    public required DbSet<Route> Routes { get; set; }
    public required DbSet<RouteStop> RouteStops { get; set; }
    public required DbSet<RouteSchedulingTemplate> RouteSchedulingTemplates { get; set; }
    public required DbSet<Vehicle> Vehicles { get; set; }
    public required DbSet<ScheduledRoute> ScheduledRoutes { get; set; }
    public required DbSet<Service> Services { get; set; }
    public required DbSet<VehicleOfferedService> VehicleServices { get; set; }
    public required DbSet<Seat> Seats { get; set; }
    //public required DbSet<User> Users { get; set; }
    public required DbSet<ScheduledRouteSeat> ScheduledRouteSeats { get; set; }
    public required DbSet<RouteTicket> RouteTickets { get; set; }
    public required DbSet<UserLocationFavourite> UserLocationFavourites { get; set; }
    public required DbSet<UserConnectionFavourite> UserConnectionFavourites { get; set; }

    private static void RenameTables(ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            // Set the table name to be the same as the entity name without pluralization
            entityType.SetTableName(entityType.ClrType.Name);
        }
    }

    private static void ApplyUtcDateTimeConversion(ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime))
                {
                    property.SetValueConverter(new ValueConverter<DateTime, DateTime>(
                        v => v.ToUniversalTime(),
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
                }
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        RenameTables(builder);
        ApplyUtcDateTimeConversion(builder);
        base.OnModelCreating(builder);

        // Relationships
        builder.Entity<Route>()
            .HasOne(r => r.StartStop)
            .WithMany()
            .HasForeignKey(r => r.StartStopId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Route>()
            .HasOne(r => r.EndStop)
            .WithMany()
            .HasForeignKey(r => r.EndStopId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<RouteStop>()
            .HasKey(rs => new { rs.RouteId, rs.StopId });
        builder.Entity<RouteStop>()
            .HasOne(rs => rs.Route)
            .WithMany(r => r.Stops)
            .HasForeignKey(rs => rs.RouteId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<RouteStop>()
            .HasOne(rs => rs.Stop)
            .WithMany(s => s.RouteStops)
            .HasForeignKey(rs => rs.StopId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<LocationStop>()
            .HasKey(ls => new { ls.LocationId, ls.StopId });
        builder.Entity<LocationStop>()
            .HasOne(ls => ls.Location)
            .WithMany(l => l.LocationStops)
            .HasForeignKey(ls => ls.LocationId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<LocationStop>()
            .HasOne(ls => ls.Stop)
            .WithMany(s => s.LocationStops)
            .HasForeignKey(ls => ls.StopId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Location>()
            .HasMany(l => l.Stops)
            .WithMany()
            .UsingEntity<LocationStop>();

        builder.Entity<ScheduledRouteSeat>()
            .HasKey(srs => new { srs.ScheduledRouteId, srs.SeatId });
        builder.Entity<ScheduledRouteSeat>()
            .HasOne(srs => srs.ScheduledRoute)
            .WithMany()
            .HasForeignKey(srs => srs.ScheduledRouteId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<ScheduledRouteSeat>()
            .HasOne(srs => srs.Seat)
            .WithMany()
            .HasForeignKey(srs => srs.SeatId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<ScheduledRouteSeat>()
            .HasOne(srs => srs.ReservedBy)
            .WithMany()
            .HasForeignKey(srs => srs.ReservedById)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<ScheduledRouteSeat>()
            .HasOne(srs => srs.RouteTicket)
            .WithMany()
            .HasForeignKey(srs => srs.RouteTicketId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<RouteTicket>()
            .HasOne(rt => rt.ScheduledRoute)
            .WithMany()
            .HasForeignKey(rt => rt.ScheduledRouteId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<RouteTicket>()
            .HasOne(rt => rt.User)
            .WithMany()
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<VehicleOfferedService>()
            .HasKey(vs => new { vs.VehicleId, vs.ServiceId });
        builder.Entity<VehicleOfferedService>()
            .HasOne(vs => vs.Vehicle)
            .WithMany()
            .HasForeignKey(vs => vs.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<VehicleOfferedService>()
            .HasOne(vs => vs.Service)
            .WithMany()
            .HasForeignKey(vs => vs.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Service>()
            .HasOne(sv => sv.Icon)
            .WithMany()
            .HasForeignKey(sv => sv.IconId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<UserLocationFavourite>()
            .HasKey(ulf => new { ulf.UserId, ulf.LocationId });
        builder.Entity<UserLocationFavourite>()
            .HasOne(ulf => ulf.User)
            .WithMany()
            .HasForeignKey(ulf => ulf.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<UserLocationFavourite>()
            .HasOne(ulf => ulf.Location)
            .WithMany()
            .HasForeignKey(ulf => ulf.LocationId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<UserConnectionFavourite>()
            .HasKey(ulf => new { ulf.UserId, ulf.FromLocationId, ulf.ToLocationId });
        builder.Entity<UserConnectionFavourite>()
            .HasOne(ulf => ulf.User)
            .WithMany()
            .HasForeignKey(ulf => ulf.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<UserConnectionFavourite>()
            .HasOne(ulf => ulf.ToLocation)
            .WithMany()
            .HasForeignKey(ulf => ulf.ToLocationId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<UserConnectionFavourite>()
            .HasOne(ulf => ulf.FromLocation)
            .WithMany()
            .HasForeignKey(ulf => ulf.FromLocationId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Entity<ScheduledRoute>()
            .HasIndex(sr => new { sr.StartTime, sr.RouteId })
            .IsUnique();
        builder.Entity<ScheduledRoute>()
            .HasOne(sr => sr.Route)
            .WithMany()
            .IsRequired();
    }
}
