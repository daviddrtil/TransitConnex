using Microsoft.EntityFrameworkCore;
using TransitConnex.API.Extensions;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.Infrastructure.Persistence;
using TransitConnex.Infrastructure.Repositories;
using TransitConnex.Infrastructure.Repositories.Interfaces;
using TransitConnex.Query;
using TransitConnex.Infrastructure.Seeds;
using TransitConnex.Infrastructure.Services;
using TransitConnex.Infrastructure.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), options =>
    {
        options.EnableRetryOnFailure();
    }));

builder.Services.AddReadDbContext();
builder.Services.AddReadOnlyRepositories();

// Handlers
// builder.Services.AddScoped(typeof(IBaseCommandHandler<>));
builder.Services.AddScoped<VehicleCommandHandler>();

// Repositories
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IIconRepository, IconRepository>();
builder.Services.AddScoped<ILineRepository, LineRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IRouteRepository, RouteRepository>();
builder.Services.AddScoped<IRouteSchedulingTemplateRepository, RouteSchedulingTemplateRepository>();
builder.Services.AddScoped<IRouteTicketRepository, RouteTicketRepository>();
builder.Services.AddScoped<IScheduledRouteRepository, ScheduledRouteRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IStopRepository, StopRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
            
// Services
builder.Services.AddScoped<IIconService, IconService>();
builder.Services.AddScoped<ILineService, LineService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IRouteService, RouteService>();
builder.Services.AddScoped<IRouteSchedulingTemplateService, RouteSchedulingTemplateService>();
builder.Services.AddScoped<IRouteTicketService, RouteTicketService>();
builder.Services.AddScoped<IScheduledRouteService, ScheduledRouteService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IStopService, StopService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.MigrateDatabasesAsync();
    
    app.UseSwagger();
    app.UseSwaggerUI();
}

var seedDb = builder.Configuration.GetValue<bool>("SeedDatabase");
var unseedDb = builder.Configuration.GetValue<bool>("UnseedDatabase");

if (unseedDb)
{
    using IServiceScope scope = app.Services.CreateScope();
    AppDbContext dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbCleaner.DeleteEntireDb(dbContext);
    return;
}

if (seedDb)
{
    DbSeeder.SeedAll(app.Services);
    return;
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
