using AuctionService.Data;
using AuctionService.Mapping;
using Mapster;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AuctionDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

TypeAdapterConfig.GlobalSettings.Scan(typeof(RegisterHelper).Assembly);

var app = builder.Build();

app.MapControllers();

await DbInitializer.InitDb(app);
await app.RunAsync();
