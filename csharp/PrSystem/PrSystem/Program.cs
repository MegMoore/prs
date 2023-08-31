using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PrSystem.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PrSystemContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PrSystemContext") ?? throw new InvalidOperationException("Connection string 'PrSystemContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
