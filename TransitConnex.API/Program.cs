using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TransitConnex.API.Automapping;
using TransitConnex.API.Configuration;
using TransitConnex.API.Middleware;
using TransitConnex.Command.Data;
using TransitConnex.Command.Seeds;
using TransitConnex.Command;
using TransitConnex.Domain.Models;
using TransitConnex.Query;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Verbose logging
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddDebug();
});

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
builder.Services.AddCommandHandlers();
builder.Services.AddQueryHandlers();

// Sql services
builder.Services.AddRepositories();
builder.Services.AddServices();

// MongoDb services
builder.Services.AddMongoDbContext();
builder.Services.AddMongoDbRepositories();
builder.Services.AddMongoDbServices();

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
        await app.MigrateSqlDbAsync();
        app.Logger.LogInformation("Database migration completed.");
    }
    if (seedDb)
    {
        await DbSeeder.SeedAll(app.Services);
        app.Logger.LogInformation("Database seeding completed.");
    }
    return;
}

await app.MigrateMongoDbAsync();    // todo delete later

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
