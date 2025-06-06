using HotelBookingPlatform.Core.Abstractions.Repositories;
using HotelBookingPlatform.Core.Hotels.Commands;
using HotelBookingPlatform.Core.Hotels.Queries;
using HotelBookingPlatform.Infrastructure.Persistence;
using HotelBookingPlatform.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddDbContext<HotelBookingDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IHotelRepository, HotelRepository>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        typeof(Program).Assembly,                      
        typeof(GetHotelById).Assembly,            
        typeof(CreateHotel).Assembly            
    );
});

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();