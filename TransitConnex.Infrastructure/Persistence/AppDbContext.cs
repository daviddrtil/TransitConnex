using Microsoft.EntityFrameworkCore;
using TransitConnex.Domain.Models;
using Route = TransitConnex.Domain.Models.Route;

namespace TransitConnex.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Icon> Icons { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Stop> Stops { get; set; }
        public DbSet<LocationStop> LocationStops { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<RouteStop> RouteStops { get; set; }
        public DbSet<RouteSchedulingTemplate> RouteSchedulingTemplates { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<ScheduledRoute> ScheduledRoutes { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<VehicleService> VehicleServices { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ScheduledRouteSeat> ScheduledRouteSeats { get; set; }
        public DbSet<RouteTicket> RouteTickets { get; set; }
        public DbSet<UserLocationFavourite> UserLocationFavourites { get; set; }
        public DbSet<UserLineFavourite> UserLineFavourites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mappings to db tables
            modelBuilder.Entity<Icon>().ToTable("Icon");
            modelBuilder.Entity<Location>().ToTable("Location");
            modelBuilder.Entity<Stop>().ToTable("Stop");
            modelBuilder.Entity<LocationStop>().ToTable("Location_Stop");
            modelBuilder.Entity<Line>().ToTable("Line");
            modelBuilder.Entity<Route>().ToTable("Route");
            modelBuilder.Entity<RouteStop>().ToTable("Route_Stop");
            modelBuilder.Entity<RouteSchedulingTemplate>().ToTable("RouteSchedulingTemplate");
            modelBuilder.Entity<Vehicle>().ToTable("Vehicle");
            modelBuilder.Entity<ScheduledRoute>().ToTable("ScheduledRoute");
            modelBuilder.Entity<Service>().ToTable("Service");
            modelBuilder.Entity<VehicleService>().ToTable("Vehicle_Service");
            modelBuilder.Entity<Seat>().ToTable("Seat");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<ScheduledRouteSeat>().ToTable("ScheduledRoute_Seat");
            modelBuilder.Entity<RouteTicket>().ToTable("RouteTicket");
            modelBuilder.Entity<UserLocationFavourite>().ToTable("User_Location_Favourite");
            modelBuilder.Entity<UserLineFavourite>().ToTable("User_Line_Favourite");
            
            
            // Composite Keys and Relationships

            // LocationStop (Composite Key)
            modelBuilder.Entity<LocationStop>()
                .HasKey(ls => new { ls.LocationId, ls.StopId });
            modelBuilder.Entity<LocationStop>()
                .HasOne(ls => ls.Location)
                .WithMany()
                .HasForeignKey(ls => ls.LocationId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<LocationStop>()
                .HasOne(ls => ls.Stop)
                .WithMany()
                .HasForeignKey(ls => ls.StopId)
                .OnDelete(DeleteBehavior.Cascade);

            // RouteStop (Composite Key)
            modelBuilder.Entity<RouteStop>()
                .HasKey(rs => new { rs.RouteId, rs.StopId });
            modelBuilder.Entity<RouteStop>()
                .HasOne(rs => rs.Route)
                .WithMany()
                .HasForeignKey(rs => rs.RouteId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<RouteStop>()
                .HasOne(rs => rs.Stop)
                .WithMany()
                .HasForeignKey(rs => rs.StopId)
                .OnDelete(DeleteBehavior.Cascade);

            // VehicleService (Composite Key)
            modelBuilder.Entity<VehicleService>()
                .HasKey(vs => new { vs.VehicleId, vs.ServiceId });
            modelBuilder.Entity<VehicleService>()
                .HasOne(vs => vs.Vehicle)
                .WithMany()
                .HasForeignKey(vs => vs.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<VehicleService>()
                .HasOne(vs => vs.Service)
                .WithMany()
                .HasForeignKey(vs => vs.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // ScheduledRouteSeat (Composite Key)
            modelBuilder.Entity<ScheduledRouteSeat>()
                .HasKey(srs => new { srs.ScheduledRouteId, srs.SeatId });
            modelBuilder.Entity<ScheduledRouteSeat>()
                .HasOne(srs => srs.ScheduledRoute)
                .WithMany()
                .HasForeignKey(srs => srs.ScheduledRouteId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ScheduledRouteSeat>()
                .HasOne(srs => srs.Seat)
                .WithMany()
                .HasForeignKey(srs => srs.SeatId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ScheduledRouteSeat>()
                .HasOne(srs => srs.ReservedBy)
                .WithMany()
                .HasForeignKey(srs => srs.ReservedById)
                .OnDelete(DeleteBehavior.Cascade);

            // UserLocationFavourite (Composite Key)
            modelBuilder.Entity<UserLocationFavourite>()
                .HasKey(ulf => new { ulf.UserId, ulf.LocationId });
            modelBuilder.Entity<UserLocationFavourite>()
                .HasOne(ulf => ulf.User)
                .WithMany()
                .HasForeignKey(ulf => ulf.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserLocationFavourite>()
                .HasOne(ulf => ulf.Location)
                .WithMany()
                .HasForeignKey(ulf => ulf.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            // UserLineFavourite (Composite Key)
            modelBuilder.Entity<UserLineFavourite>()
                .HasKey(ulf => new { ulf.UserId, ulf.LineId });
            modelBuilder.Entity<UserLineFavourite>()
                .HasOne(ulf => ulf.User)
                .WithMany()
                .HasForeignKey(ulf => ulf.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserLineFavourite>()
                .HasOne(ulf => ulf.Line)
                .WithMany()
                .HasForeignKey(ulf => ulf.LineId)
                .OnDelete(DeleteBehavior.Cascade);

            // RouteTicket Relationships
            modelBuilder.Entity<RouteTicket>()
                .HasOne(rt => rt.User)
                .WithMany()
                .HasForeignKey(rt => rt.UserId);
            modelBuilder.Entity<RouteTicket>()
                .HasOne(rt => rt.Route)
                .WithMany()
                .HasForeignKey(rt => rt.RouteId);
            modelBuilder.Entity<RouteTicket>()
                .HasOne(rt => rt.Seat)
                .WithMany()
                .HasForeignKey(rt => rt.SeatId);
        }
    }
}
