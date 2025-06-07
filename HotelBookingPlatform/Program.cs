using HotelBookingPlatform.Infrastructure.Persistence;
using HotelBookingPlatform.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddDbContext<HotelBookingDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<HotelRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
