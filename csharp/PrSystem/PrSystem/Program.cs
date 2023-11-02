using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PrSystem.Data;
var builder = WebApplication.CreateBuilder(args);

var connStrKey = "ProdDb";

#if DEBUG
    connStrKey = "DevDb";

#endif
builder.Services.AddDbContext<PrSystemContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProdDb") ?? throw new InvalidOperationException("Connection string 'PrSystemContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
