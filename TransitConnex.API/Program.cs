using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TransitConnex.API.Configuration;
using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.API.Handlers.QueryHandlers;
using TransitConnex.API.Middleware;
using TransitConnex.Command.Data;
using TransitConnex.Command.Repositories;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Command.Seeds;
using TransitConnex.Command.Services;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.Automapping;
using TransitConnex.Domain.Models;
using TransitConnex.Query;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), options =>
    {
        options.EnableRetryOnFailure();
    }));

// User authentication
builder.Services.AddIdentity<User, IdentityRole<Guid>>(
        IdentityConfiguration.ConfigureIdentityOptions)
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Handlers
// builder.Services.AddScoped(typeof(IBaseCommandHandler<>));
builder.Services.AddScoped<IconCommandHandler>();
// builder.Services.AddScoped<LineCommandHandler>();
// builder.Services.AddScoped<LocationCommandHandler>();
// builder.Services.AddScoped<RouteCommandHandler>();
// builder.Services.AddScoped<RouteSchedulingTemplateCommandHandler>();
// builder.Services.AddScoped<RouteTicketCommandHandler>();
// builder.Services.AddScoped<ScheduledRouteCommandHandler>();
// builder.Services.AddScoped<SeatCommandHandler>();
builder.Services.AddScoped<ServiceCommandHandler>();
// builder.Services.AddScoped<StopCommandHandler>();
// builder.Services.AddScoped<UserCommandHandler>();
builder.Services.AddScoped<VehicleCommandHandler>();
builder.Services.AddScoped<VehicleRTIQueryHandler>();

// Repositories
builder.Services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
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

// MongoDb services DI
builder.Services.AddReadDbContext();
builder.Services.AddMongoDbRepositories();
builder.Services.AddMongoDbServices();

// Verbose logging
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddDebug();
});

var app = builder.Build();

bool updateDb = builder.Configuration.GetValue<bool>("UpdateDb");
bool seedDb = builder.Configuration.GetValue<bool>("SeedDatabase");
bool unseedDb = builder.Configuration.GetValue<bool>("UnseedDatabase");
if (updateDb || seedDb || unseedDb)
{
    if (unseedDb)
    {
        DbCleaner.DeleteEntireDb(app.Services);
        app.Logger.LogInformation("Database cleaning completed.");
    }

    if (updateDb)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
        app.Logger.LogInformation("Database migration completed.");
    }

    if (seedDb)
    {
        await DbSeeder.SeedAll(app.Services);
        app.Logger.LogInformation("Database seeding completed.");
    }
    return;
}
await app.MigrateDatabasesAsync();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
